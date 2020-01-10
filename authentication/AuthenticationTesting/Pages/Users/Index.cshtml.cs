using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthenticationTesting.Data;
using AuthenticationTesting.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthenticationTesting.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AuthenticationTesting.Data.UserContext _context;

        public IndexModel(AuthenticationTesting.Data.UserContext context)
        {
            _context = context;
        }


        
        public static byte[] getHashBytes(string input){
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string getHashString(string input){
            StringBuilder builder = new StringBuilder();
            foreach(byte b in getHashBytes(input)){
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }

        public IList<User> User { get;set; }


        [BindProperty(SupportsGet = true)]
        public string username { get;set;}

        [BindProperty(SupportsGet = true)]
        public string password { get;set;}


        public async Task OnGetAsync()
        {
            //search for specific user
            var users = from u in _context.User select u;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)){
                users = users.Where(u => u.Username.Equals(username) && u.Password.Equals(password));
            }
            User = await users.ToListAsync();
        }
    }
}
