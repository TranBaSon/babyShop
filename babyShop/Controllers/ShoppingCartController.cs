using babyShop.Data;
using babyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace babyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MyDbContext db = new MyDbContext();
        
        private const string ShoppingCartSessionName = "SHOPPING_CART";
        
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == productId);
            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }
            var shoppingCart = GetShoppingCart();
            shoppingCart.Add(existingProduct, quantity, false);
            SetShoppingCart(shoppingCart);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ShowCart()
        {
            return View("ShowCart", GetShoppingCart());
        }


        public ActionResult UpdateCart(int productId, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == productId);
            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }
            var shoppingCart = GetShoppingCart();
            shoppingCart.Update(existingProduct, quantity);
            SetShoppingCart(shoppingCart);
            return null;
        }


        public ActionResult RemoveCartItem(int productId)
        {
            var shoppingCart = GetShoppingCart();
            shoppingCart.Remove(productId);
            SetShoppingCart(shoppingCart);
            return RedirectToAction("ShowCart");
        }

        public ActionResult RemoveAll()
        {
            ClearShoppingCart();
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        private ShoppingCart GetShoppingCart()
        {
            ShoppingCart shoppingCart = null;

            if (Session[ShoppingCartSessionName] != null)
            {
                try
                {
                    shoppingCart = Session[ShoppingCartSessionName] as ShoppingCart;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }
            return shoppingCart;
        }

        private void SetShoppingCart(ShoppingCart shoppingCart)
        {
            Session[ShoppingCartSessionName] = shoppingCart;
        }

        private void ClearShoppingCart()
        {
            Session[ShoppingCartSessionName] = null;
        }
    }
}