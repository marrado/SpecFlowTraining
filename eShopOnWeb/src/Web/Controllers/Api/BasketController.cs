using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class BasketController : BaseApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService,
                                SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _basketViewModelService = basketViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserBasket()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return Ok(await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name));
            }
            string anonymousId = GetOrSetBasketCookie();
            return Ok(await _basketViewModelService.GetOrCreateBasketForUser(anonymousId));
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string username)
        {
            return Ok(await _basketViewModelService.GetOrCreateBasketForUser(username));
        }

        private string GetOrSetBasketCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            string anonymousId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions);
            return anonymousId;
        }
    }
}