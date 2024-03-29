﻿using System.Collections.Generic;
using System.Linq;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
    public class CartModel : PageModel
    {
        public List<Item> cart { get; set; }
        public double Total { get; set; }

        private BookStore.Data.BookStoreContext context;

        public CartModel(BookStore.Data.BookStoreContext _context)
        {
            context = _context;
        }

        public void OnGet()
        {
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = (double)cart.Sum(i => i.Product.Price * i.Quantity);
        }

        public IActionResult OnGetBuyNow(int id)
        {
            var Ord = context.Book.Where(a => a.ID == id).Single();

            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null)
            {
                cart = new List<Item>();
                cart.Add(new Item
                {
                    Product = Ord,
                    Quantity = 1
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new Item
                    {
                        Product = Ord,
                        Quantity = 1
                    });

                }
                else
                {
                    cart[index].Quantity++;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToPage("cart");

        }



        public IActionResult OnGetDelete(int id)
        {
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("cart");
        }

        public IActionResult OnPostUpdate(int quantities, int id)
        {
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(cart, id);
            cart[index].Quantity = quantities;

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("cart");
        }

        private int Exists(List<Item> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ID == id)
                {
                    return i;
                }

            }
            return -1;
        }

    }
}
