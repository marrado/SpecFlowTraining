using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace SpecFlowTests.Infrastructure
{
    /// <summary>
    /// Context class holding the link to the currently used database. USed to perform operations on the database.
    /// </summary>
    public class DatabaseContext
    {
        private readonly WebApplicationContext _webApplicationContext;

        public DatabaseContext(WebApplicationContext webApplicationContext)
        {
            _webApplicationContext = webApplicationContext;
        }

        /// <summary>
        /// Clears the database and checks that it succeeded.
        /// </summary>
        public void EnsureDatabaseEmpty()
        {
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       context.CatalogBrands.RemoveRange(context.CatalogBrands);
                                                                                       context.CatalogTypes.RemoveRange(context.CatalogTypes);
                                                                                       context.CatalogItems.RemoveRange(context.CatalogItems);
                                                                                       context.OrderItems.RemoveRange(context.OrderItems);
                                                                                       context.Orders.RemoveRange(context.Orders);
                                                                                       context.Baskets.RemoveRange(context.Baskets);
                                                                                       context.SaveChanges();
                                                                                   }));

            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       context.CatalogBrands.Should().BeEmpty();
                                                                                       context.CatalogTypes.Should().BeEmpty();
                                                                                       context.CatalogItems.Should().BeEmpty();
                                                                                       context.OrderItems.Should().BeEmpty();
                                                                                       context.Orders.Should().BeEmpty();
                                                                                       context.Baskets.Should().BeEmpty();
                                                                                   }));
        }

        /// <summary>
        /// Inserts given CatalogItems and checks that it succeeded.
        /// </summary>
        /// <param name="catalogItems">Items to be inserted</param>
        public void EnsureCatalogItemsExist(List<CatalogItem> catalogItems)
        {
            if (catalogItems == null || !EnumerableExtensions.Any(catalogItems))
                throw new ArgumentException("Collection of items to be added is empty");

            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       catalogItems.ForEach(item =>
                                                                                                            {
                                                                                                                var existingItem = context.CatalogItems.SingleOrDefault(i => i.Id == item.Id);
                                                                                                                if (existingItem != null)
                                                                                                                    context.CatalogItems.Remove(existingItem);
                                                                                                            });
                                                                                       context.SaveChanges();
                                                                                       context.AddRange(catalogItems);
                                                                                       context.SaveChanges();
                                                                                   }));
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       context.CatalogItems.Where(item => catalogItems.Contains(item)).Should().HaveCount(catalogItems.Count);
                                                                                   }));
        }

        /// <summary>
        /// Ensures that the given basket has no items in the db.
        /// </summary>
        /// <param name="id"></param>
        public void EnsureBasketEmpty(int id)
        {
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       var basket = context.Baskets.Single(b => b.Id == id);
                                                                                       basket.Clear();
                                                                                       context.SaveChanges();

                                                                                       context.Baskets.Single(b => b.Id == id).Items.Should().BeEmpty();
                                                                                   }));
        }

        /// <summary>
        /// Ensures that basket contains exactly given items.
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="catalogItems"></param>
        public void EnsureBasketContainsOnlyItems(int basketId, List<CatalogItem> catalogItems)
        {
            EnsureBasketEmpty(basketId);
            EnsureCatalogItemsExist(catalogItems);

            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       var basket = context.Baskets.Single(b => b.Id == basketId);
                                                                                       catalogItems.ForEach(item => basket.AddItem(item.Id, item.Price));
                                                                                       context.SaveChanges();

                                                                                       context.Baskets.Single(b => b.Id == basketId).Items.Should().HaveCount(catalogItems.Count);
                                                                                   }));
        }
    }
}