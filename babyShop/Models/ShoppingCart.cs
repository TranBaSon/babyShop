﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace babyShop.Models
{
    public class ShoppingCart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public double TotalPrice => Items.Values.Sum(items => items.TotalItemPrice);

        public ShoppingCart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        public void Add(Product product, int quantity, bool isUpdate)
        {
            var cartItem = new CartItem()
            {
                ProductId = product.Id,
                ProductPrice = product.Price,
                ProductName = product.Name,
                ProductThumbnail = product.Thumbnail,
                Quantity = quantity
            };
            var existKey = Items.ContainsKey(product.Id);
            if (!isUpdate && existKey)
            {
                var existingItem = Items[product.Id];
                cartItem.Quantity += existingItem.Quantity;
            }
            if (existKey)
            {
                Items[product.Id] = cartItem;
            }
            else
            {
                Items.Add(product.Id, cartItem);
            }
        }

        public void Add(Product product) 
        {
            Add(product, 1, false);
        }

        public void Update(Product product, int quantity)
        {
            Add(product, quantity, true);
        }

        public void Remove(int productId) 
        {
            if (Items.ContainsKey(productId))
            {
                Items.Remove(productId);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; } 
        public string ProductThumbnail { get; set; } 
        public double ProductPrice { get; set; } 
        public double TotalItemPrice => ProductPrice * Quantity;
        public int Quantity { get; set; }

    }
}
