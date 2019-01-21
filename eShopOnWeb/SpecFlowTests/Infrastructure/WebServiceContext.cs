using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.ViewModels;
using Newtonsoft.Json;

namespace SpecFlowTests.Infrastructure
{
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

        private void Post<TModel>(string path, TModel data)
        {
            var response = Task.Run(() => _webApplicationContext.Client.PostAsync(path, data, new JsonMediaTypeFormatter())).Result;
            response.EnsureSuccessStatusCode();
        }

        //Hack mit Get, because of problems with post
        public void AddToBasket(CatalogItemViewModel item)
        {
            Get<object>($"/Basket/AddToBasket?productId={item.Id}&price={item.Price}");
        }

        public BasketViewModel GetBasket()
        {
            return Get<BasketViewModel>("api/basket/GetCurrentUserBasket");
        }
    }
}