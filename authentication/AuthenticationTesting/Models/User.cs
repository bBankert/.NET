using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationTesting.Models{
    public class User{
        public int ID {get;set;}

        
        [MinLength(1),Required]
        public string Name {get;set;}

        [MinLength(1),Required]      
        public string Username {get;set;}

        [MinLength(1),Required]
        public string Password {get;set;}
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {get;set;}
    }
}