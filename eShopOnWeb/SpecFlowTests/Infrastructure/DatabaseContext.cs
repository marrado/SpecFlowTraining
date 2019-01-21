using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
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
        public void EnsureCatalogItemsExist(ICollection<CatalogItem> catalogItems)
        {
            if (catalogItems == null || !catalogItems.Any())
                throw new ArgumentException("Collection of items to be added is empty");

            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       context.AddRange(catalogItems);
                                                                                       context.SaveChanges();
                                                                                   }));
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context => { context.CatalogItems.Should().HaveCount(catalogItems.Count); }));
        }

        /// <summary>
        /// Ensures that no baskets are saved in the db.
        /// </summary>
        public void EnsureNoBasketsSaved()
        {
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
                                                                                   {
                                                                                       context.Baskets.RemoveRange(context.Baskets);
                                                                                       context.SaveChanges();

                                                                                       context.Baskets.Should().BeEmpty();
                                                                                   }));
        }
    }
}