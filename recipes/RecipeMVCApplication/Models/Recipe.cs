using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace RecipeApplication.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        public int ID { get; set; }

        [Required,MinLength(1)]
        public string Name { get; set; }

        [DataType(DataType.Date),Display(Name ="Creation Date")]
        public DateTime Date { get; set; }

        

    }

    public class RecipeDBContext : DbContext
    {

        public DbSet<Recipe> Recipes { get; set; }
    }


}