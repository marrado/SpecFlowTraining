using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Web.ViewModels;

namespace SpecFlowTests.Infrastructure
{
    /// <summary>
    /// Helper class, which provides with valid dummy items.
    /// </summary>
    public static class TestDataProvider
    {
        /// <summary>
        /// Returns a list of dummy CatalogItems
        /// </summary>
        /// <param name="itemsCount"></param>
        /// <param name="catalogTypeId"></param>
        /// <param name="catalogBrandId"></param>
        /// <returns></returns>
        public static List<CatalogItem> GetDummyCatalogItems(int itemsCount, int catalogTypeId = 1, int catalogBrandId = 2)
        {
            var items = new List<CatalogItem>();
            for (int i = 0; i < itemsCount; i++)
            {
                items.Add(GetDummyCatalogItem(i + 1, catalogTypeId, catalogBrandId));
            }

            return items;
        }

        /// <summary>
        /// Returns a dummy CatalogItem
        /// </summary>
        /// <returns></returns>
        public static CatalogItem GetDummyCatalogItem(int itemId = 1, int catalogTypeId = 1, int catalogBrandId = 2)
        {
            Random r = new Random();
            return new CatalogItem
                   {
                       Price = r.Next() % 50,
                       PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png",
                       Id = itemId,
                       Name = "TestItem"
                   };
        }
        /// <summary>
        /// Returns a list of dummy CatalogItemViewModels
        /// </summary>
        /// <param name="itemsCount"></param>
        /// <param name="catalogTypeId"></param>
        /// <param name="catalogBrandId"></param>
        /// <returns></returns>
        public static List<CatalogItemViewModel> GetDummyCatalogItemViewModels(int itemsCount)
        {
            var items = new List<CatalogItemViewModel>();
            for (int i = 0; i < itemsCount; i++)
            {
                items.Add(GetDummyCatalogItemViewModel(i + 1));
            }

            return items;
        }

        /// <summary>
        /// Returns a dummy CatalogItemViewModel
        /// </summary>
        /// <returns></returns>
        public static CatalogItemViewModel GetDummyCatalogItemViewModel(int itemId = 1)
        {
            Random r = new Random();
            return new CatalogItemViewModel
                   {
                       Price = r.Next() % 50,
                       PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png",
                       Id = itemId,
                       Name = "TestItem"
                   };
        }
    }
}