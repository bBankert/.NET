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
using Microsoft.AspNetCore.Http.Extensions; 

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


        //query params
        [BindProperty(SupportsGet = true)]
        public string username { get;set;}

        [BindProperty(SupportsGet = true)]
        public string password { get;set;}


        public async Task OnGetAsync()
        {

            //get username & password query params
            string username_query = Request.Query["username"];
            string password_query = Request.Query["password"];
            //change to restful url
            string new_path = "users/"+username_query+'/'+password_query;
            string current_path = HttpContext.Request.GetEncodedUrl();
            //makes sure there are query params and if so, change to routing
            if((!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            && current_path.Contains('?')){
                //immediate redirect to proper path if query params exist
                Response.Redirect(new_path);                
            }





            //search for specific user
            var users = from u in _context.User select u;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)){
                users = users.Where(u => u.Username.Equals(username) && u.Password.Equals(password));
                
            }

            //get eveything for testing
            if(username.Equals("admin") && password.Equals("123")){
                users = _context.User;
            }


            //if no users from login redirect to login page
            if(users.Count() == 0){
                Response.Redirect("../Index");
            }

            User = await users.ToListAsync();



        }
    }
}
