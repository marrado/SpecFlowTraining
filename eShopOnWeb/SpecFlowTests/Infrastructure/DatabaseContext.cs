using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        public void EnsureDatabaseIsEmpty()
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

            AssertDatabaseIsEmpty();
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

            AssertCatalogItemsExist(catalogItems);
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

            var basketOriginalItemCount = 0;
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                var basket = context.Baskets
                    .Include(b => b.Items)
                    .Single(b => b.Id == basketId);

                basketOriginalItemCount = basket.ItemCount;

                catalogItems.ForEach(item => basket.AddItem(item.Id, item.Price));
                context.SaveChanges();
            }));

            AssertBasketHasItemCount(basketId, basketOriginalItemCount + catalogItems.Count);
        }

        public void EnsureBasketExists(string username)
        {
            if (GetBasketForUser(username) != null)
                return;

            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                var id = context.Baskets.Any() ? context.Baskets.Max(b => b.Id) : 0;
                id++;
                context.Baskets.Add(new Basket
                {
                    BuyerId = username,
                    Id = id
                });
                context.SaveChanges();

                AssertBasketExists(username, context);
            }));
        }

        public Basket GetBasketForUser(string username)
        {
            Basket basket = null;
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                basket = context.Baskets
                    .Include(b => b.Items)
                    .SingleOrDefault(b => b.BuyerId == username);
            }));

            return basket;
        }

        public int GetBasketId(string username)
        {
            return GetBasketForUser(username)?.Id ?? -1;
        }

        private void AssertCatalogItemsExist(List<CatalogItem> catalogItems)
        {
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                context.CatalogItems.Where(item => catalogItems.Contains(item)).Should()
                    .HaveCount(catalogItems.Count, "Not all the expected items exist in the database.");
            }));
        }

        private void AssertBasketHasItemCount(int basketId, int count)
        {
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                context.Baskets
                    .Include(b => b.Items)
                    .Single(b => b.Id == basketId)
                    .Items.Should().HaveCount(count, $"Basket with id '{basketId}' was expected to contain '{count}' items, but it does not.");
            }));
        }

        private void AssertDatabaseIsEmpty()
        {
            const string message = "The database was expected to be empty, but is not.";
            _webApplicationContext.PerformServiceAction(new Action<CatalogContext>(context =>
            {
                context.CatalogBrands.Should().BeEmpty(message);
                context.CatalogTypes.Should().BeEmpty(message);
                context.CatalogItems.Should().BeEmpty(message);
                context.OrderItems.Should().BeEmpty(message);
                context.Orders.Should().BeEmpty(message);
                context.Baskets.Should().BeEmpty(message);
            }));
        }

        private void AssertBasketExists(string username, CatalogContext context)
        {
            context.Baskets
                .Count(b => b.BuyerId == username).Should().Be(1, $"The user with id '{username}'should have a basket, but none was found.");
        }
    }
}