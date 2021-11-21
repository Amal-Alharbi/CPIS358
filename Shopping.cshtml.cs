using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
    public class ShoppingModel : PageModel
    {

        public string WelcomeUsername { get; set; }

        private BookStore.Data.BookStoreContext context;
        public List<Book> allBooks;

        public ShoppingModel(BookStore.Data.BookStoreContext _context)
        {
            context = _context;
        }



        public void OnGet()
        {
            WelcomeUsername = HttpContext.Session.GetString("username");

            allBooks = context.Book.ToList();

        }

        public IActionResult OnGetLogout()
        {

            HttpContext.Session.Remove("username");

            return RedirectToPage("Index");
        }

    }
}
