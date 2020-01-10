using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Web;
using AuthenticationTesting.Models;

namespace AuthenticationTesting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AuthenticationTesting.Data.UserContext _context; 
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string username;
        public string password;

        
    }
}
