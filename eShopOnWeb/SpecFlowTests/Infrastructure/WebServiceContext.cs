using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.ViewModels;
using Newtonsoft.Json;

namespace SpecFlowTests.Infrastructure
{
    /// <summary>
    /// Wrapper class to hide the technicalities from the driver classes.
    /// </summary>
    public class WebServiceContext
    {
        private readonly WebApplicationContext _webApplicationContext;

        public WebServiceContext(WebApplicationContext webApplicationContext)
        {
            _webApplicationContext = webApplicationContext;
        }

        public CatalogIndexViewModel GetFirstCatalogPage(int pageSize = Constants.DefaultCatalogPageSize)
        {
            if(pageSize != Constants.DefaultCatalogPageSize)
                return Get<CatalogIndexViewModel>($"/api/catalog/list?pageSize={pageSize}");
            return Get<CatalogIndexViewModel>("/api/catalog/list");
        }

        public CatalogIndexViewModel GetCatalogPage(int pageNumber, int pageSize = Constants.DefaultCatalogPageSize)
        {
            return Get<CatalogIndexViewModel>($"/api/catalog/list?page={pageNumber}&pageSize={pageSize}");
        }

        public void AddToBasket(CatalogItemViewModel item)
        {
            var dict = new Dictionary<string, string>
            {
                {"Name", item.Name},
                {"Price", item.Price.ToString(CultureInfo.InvariantCulture)},
                {"Id", item.Id.ToString()},
                {"PictureUri", item.PictureUri}
            };
            Post($"/Basket/AddToBasket", new FormUrlEncodedContent(dict));
        }

        public BasketViewModel GetBasket()
        {
            return Get<BasketViewModel>("api/basket/GetCurrentUserBasket");
        }

        private TModel Get<TModel>(string path)
        {
            var response = Task.Run(() => _webApplicationContext.Client.GetAsync(path)).Result;
            if (response.StatusCode == HttpStatusCode.Found)
            {
                return default(TModel);
            }

            response.EnsureSuccessStatusCode();
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TModel>(stringResponse);
        }

        private void Post(string path, HttpContent data)
        {
            var response = Task.Run(() => _webApplicationContext.Client.PostAsync(path, data)).Result;
            if (response.StatusCode == HttpStatusCode.Found)
            {
                return;
            }
            response.EnsureSuccessStatusCode();
        }
    }
}