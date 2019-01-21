using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Web.ViewModels;

namespace SpecFlowTests.Infrastructure
{
    public static class TestDataProvider
    {
        public static List<CatalogItem> GetDummyCatalogItems(int itemsCount, int catalogTypeId = 1, int catalogBrandId = 2)
        {
            Random r = new Random();
            var items = new List<CatalogItem>();
            for (int i = 0; i < itemsCount; i++)
            {
                items.Add(new CatalogItem
                          {
                              CatalogTypeId = catalogTypeId,
                              CatalogBrandId = catalogBrandId,
                              Description = $"TestItem{i}",
                              Price = r.Next() % 50,
                              PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png"
                });
            }

            return items;
        }

        public static CatalogItem GetDummyCatalogItem()
        {
            Random r = new Random();
            return new CatalogItem
                   {
                       Price = r.Next() % 50,
                       PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png",
                       Id = 1,
                       Name = "TestItem"
                   };
        }

        public static CatalogItemViewModel GetDummyCatalogItemViewModel()
        {
            Random r = new Random();
            return new CatalogItemViewModel
                   {
                       Price = r.Next() % 50,
                       PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png",
                       Id = 1,
                       Name = "TestItem"
                   };
        }
    }
}