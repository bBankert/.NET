namespace RecipeDatabasesApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientID);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeID);
            
            CreateTable(
                "dbo.UserRecipes",
                c => new
                    {
                        UserRecipeID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RecipeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRecipeID)
                .ForeignKey("dbo.Recipes", t => t.RecipeID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RecipeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRecipes", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRecipes", "RecipeID", "dbo.Recipes");
            DropIndex("dbo.UserRecipes", new[] { "RecipeID" });
            DropIndex("dbo.UserRecipes", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRecipes");
            DropTable("dbo.Recipes");
            DropTable("dbo.Ingredients");
        }
    }
}
