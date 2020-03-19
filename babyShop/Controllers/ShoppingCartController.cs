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
        // Tên của shopping cart trong session.
        private const string ShoppingCartSessionName = "SHOPPING_CART";
        // Action có tên là AddToCart. Có tham số truyền vào là id sản phẩm và số lượng muốn cho vào giỏ hàng.
        // Thêm một sản phẩm vào cart
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            // Check product có tồn tại không?
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == productId);
            if (existingProduct == null)
            {
                // trả về 404
                return new HttpNotFoundResult();
            }
            var shoppingCart = GetShoppingCart();
            shoppingCart.Add(existingProduct, quantity, false);
            SetShoppingCart(shoppingCart);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // hiển thị danh sách sản phẩm được thêm vào shopping cart.
        public ActionResult ShowCart()
        {
            return View("ShowCart", GetShoppingCart());
        }


        public ActionResult UpdateCart(int productId, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == productId);
            if (existingProduct == null)
            {
                // trả về 404
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
            // Kiểm tra sự tồn tại của sc(shopping cart) trong session.
            if (Session[ShoppingCartSessionName] != null)
            {
                // nếu có
                try
                {
                    // ép kiểu đối tượng lấy được về kiểu ShoppingCart.
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