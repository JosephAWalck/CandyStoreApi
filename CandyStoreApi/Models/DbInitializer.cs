namespace CandyStoreApi.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            CandyStoreApiContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<CandyStoreApiContext>();
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Candies.Any())
            {
                context.AddRange
                (
                    new Candy { 
                        Name = "Hershey's Milk Chocolate Candy Bar (1.55 oz)", 
                        Description = "\"The Great American Chocolate Bar\" first sold in 1900 is still a favorite of young and old alike. Can't go wrong with an American tradition. Great for those s'mores, goodie bags, lunch bags, and plain ole snackin'.", 
                        Price = 1.75M, 
                        ImageURL = "TODO", 
                        Inventory = 250, 
                        Category = Categories["Chocolate Candy"] },
                    new Candy
                    {
                        Name = "Lindt Milk Chocolate CLASSIC RECIPE Bar (4.4 oz)",
                        Description = "Indulge in the smooth, creamy taste of a Lindt CLASSIC RECIPE Milk Chocolate Bar. Expertly crafted, this milk chocolate candy bar offers classic rich flavor and smooth texture. Enjoy this indulgent treat after dinner, or use these milk chocolate bars in your baking for delicious results. Made with the finest ingredients, this refined candy bar also makes a delicious gift for any chocolate lover.",
                        Price = 4.89M,
                        ImageURL = "TODO",
                        Inventory = 200,
                        Category = Categories["Chocolate Candy"]
                    },
                    new Candy
                    {
                        Name = "DOVE Candy Milk Chocolate Bar, (3.30 oz)",
                        Description = "DOVE Milk Chocolate Bars will brighten your day. Each candy bar comes individually wrapped, but is segmented into pieces that you can break off and share with friends. Whether savored a piece at a time or enjoyed as a deliciously simple dessert, these extra-large chocolate bars are the perfect way to celebrate wins big and small.",
                        Price = 3.59M,
                        ImageURL = "TODO",
                        Inventory = 300,
                        Category = Categories["Chocolate Candy"]
                    },
                    new Candy
                    {
                        Name = "Jolly Rancher Hard Candy Original Flavors (7 oz. Bag)",
                        Description = "Grab a bag of the original Jolly Ranchers to take with you on the go! Individually wrapped Jolly Rancher hard candies make it easy to share with friends and family! Bag contains original fruity flavors: Green Apple, Cherry, Watermelon, Grape and Blue Raspberry.",
                        Price = 4.25M,
                        ImageURL = "TODO",
                        Inventory = 150,
                        Category = Categories["Hard Candy"]
                    },
                    new Candy
                    {
                        Name = "Werther’s Original Caramel Hard Candies (2.65-oz. Bag)",
                        Description = "Our classic Werther’s Original Caramel Hard Candies are still made in the original family recipe, using ingredients like real butter and fresh cream. This is the Original Caramel, the ones that are seen being handed out by grandparents left and right. Start carrying a few in your pockets right now and you may find that having a little sweet treat a few times a day makes the day a little more pleasant.",
                        Price = 2.50M,
                        ImageURL = "TODO",
                        Inventory = 100,
                        Category = Categories["Hard Candy"]
                    },
                    new Candy
                    {
                        Name = "Lemonhead Lemon Unwrapped Candy (4.5 oz. Bag)",
                        Description = "What's round, yellow and sweetly sour? Lemonheads -- small, sour-lemon-flavored balls, wrapped in a sweet coat of sugar and surrounded by a chewy sour lemon shell.",
                        Price = 3.00M,
                        ImageURL = "TODO",
                        Inventory = 150,
                        Category = Categories["Hard Candy"]
                    },
                    new Candy
                    {
                        Name = "Haribo Goldbears (10 oz. Bag)",
                        Description = "The original gummi bear. Five flavors: Raspberry (red bear), Orange (orange bear), Lemon (yellow bear), Pineapple (clear bear) and Strawberry (green bear). These gummi bears are the original soft and chewy bursting with flavor!",
                        Price = 7.00M,
                        ImageURL = "TODO",
                        Inventory = 200,
                        Category = Categories["Gummi Candy"]
                    },
                    new Candy
                    {
                        Name = "Trolli Sour Brite Crawlers Gummi Worm Candy - 5-oz. Bag",
                        Description = "Both kids and adults favor Sour Brite Crawlers with their neon colors and stretchy texture. And of course their sour sugar coating makes them even more desirable. You don’t have to share if you don’t want to but aren’t these best when eaten with someone else? We think gummi worms are a fun candy store classic. Dig into a bag of Trolli gummi worms and chew away! ",
                        Price = 3.50M,
                        ImageURL = "TODO",
                        Inventory = 225,
                        Category = Categories["Gummi Candy"]
                    },
                    new Candy
                    {
                        Name = "Hi-Chew Original Mix Fruit Chews Bags ()3.53 oz Bag",
                        Description = "A whole bagful of your favorite original Hi-Chew flavors, including strawberry, grape and green apple.",
                        Price = 3.50M,
                        ImageURL = "TODO",
                        Inventory = 250,
                        Category = Categories["Gummi Candy"]
                    }
                );

            }
            context.SaveChanges();

        }
        private static Dictionary<string, Category>? categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Chocolate Candy" },
                        new Category { CategoryName = "Hard Candy" },
                        new Category { CategoryName = "Gummi Candy" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}
