using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Seeds
{
    public class SeedFood : IFood
    {
        private readonly ICategory _category;
        private readonly IEnumerable<Food> _foods;

        public SeedFood()
        {
            _category = new SeedCategory();
            _foods = new List<Food>
            {
                new Food
                {
                    Name = "Eggplant",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Vegetable"),
                    ImageUrl="https://images.pexels.com/photos/321551/pexels-photo-321551.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock=true,
                    IsPreferedFood = false,
                    ShortDescription = "The aubergine (also called eggplant) is a plant. Its fruit is eaten as a vegetable.",
                    LongDescription = "The plant is in the nightshade family of plants. It is related to the potato and tomato. Originally it comes from India and Sri Lanka. The Latin/French term aubergine originally derives from the historical city of Vergina (Βεργίνα) in Greece.",
                    Price = 4.5M
                },
                new Food
                {
                    Name = "Cauliflower",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Vegetable"),
                    ImageUrl = "https://images.pexels.com/photos/461245/pexels-photo-461245.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription = "Cauliflower is one of several vegetables in the species Brassica oleracea, in the family Brassicaceae.",
                    LongDescription = "Cauliflower is a variety of cabbage, whose white flower head is eaten. Cauliflower is very nutritious, and may be eaten cooked, raw or pickled. It is a popular vegetable in Poland where it is eaten in a soup with cream or fried with bread crumbs.",
                    Price = 5.3M
                },
                new Food
                {
                    Name = "Broccoli",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Vegetable"),
                    ImageUrl = "https://images.pexels.com/photos/47347/broccoli-vegetable-food-healthy-47347.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = false,
                    IsPreferedFood = true,
                    ShortDescription = "Broccoli is a plant, Brassica oleracea. It is a vegetable like cabbage.",
                    LongDescription = "Broccoli has green flower heads and a stalk. It comes from Mexico and is one of the most bought vegetables in England.",
                    Price = 3.3M
                },
                new Food
                {
                    Name = "Apple",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Fruit"),
                    ImageUrl = "https://images.pexels.com/photos/39803/pexels-photo-39803.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription="The apple tree (Malus domestica) is a tree that grows fruit (such as apples) in the rose family best known for its juicy, tasty fruit.",
                    LongDescription = "Apples are generally propagated by grafting, although wild apples grow readily from seed. Apple trees are large if grown from seed, but small if grafted onto roots (rootstock). There are more than 7,500 known cultivars of apples, with a range of desired characteristics. Different cultivars are bred for various tastes and uses: cooking, eating raw and cider production are the most common uses.",
                    Price = 2.7M
                },
                new Food
                {
                    Name ="Avocado",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Fruit"),
                    ImageUrl = "https://images.pexels.com/photos/557659/pexels-photo-557659.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = false,
                    IsPreferedFood = false,
                    ShortDescription = "An avocado is a berry fruit. It has medium dark green or dark green bumpy or smooth skin depending on the variety.",
                    LongDescription = @"The flesh of an avocado is deep chartreuse green in color near the skin and pale chartreuse green near the core. It has a creamy, rich texture.
Avocado trees come from Central America and Mexico. They can grow in many places, as long as it is not too cold.",
                    Price = 6.1M
                },
                new Food
                {
                    Name = "Banana",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Fruit"),
                    ImageUrl = "https://images.pexels.com/photos/38283/bananas-fruit-carbohydrates-sweet-38283.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription = "A banana is the common name for a type of fruit and also the name for the herbaceous plants that grow it.",
                    LongDescription = "It is thought that bananas were grown for food for the first time in Papua New Guinea.[1] Today, they are cultivated in tropical regions around the world.[2] Most banana plants are grown for their fruits, which botanically are a type of berry. Some are grown as ornamental plants, or for their fibres.",
                    Price = 4.6M
                },
                new Food
                {
                    Name = "Grapefruit",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Fruit"),
                    ImageUrl = "https://images.pexels.com/photos/209549/pexels-photo-209549.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = false,
                    ShortDescription = "Grapefruit is a citrus fruit grown in sub-tropical places.",
                    LongDescription = "The tree which the grapefruit grows on is normally 5-6 meters tall but can reach up to 15 meters tall. It has dark green leaves that measure up to 150mm and has white flowers that grow 5cm in length.",
                    Price = 6.4M
                },
                new Food
                {
                    Name = "Barley",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Grain"),
                    ImageUrl = "https://images.pexels.com/photos/533346/pexels-photo-533346.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = false,
                    ShortDescription = "Barley, a member of the grass family, is a major cereal grain grown in temperate climates globally.",
                    LongDescription = "It was one of the first cultivated grains, particularly in Eurasia as early as 10,000 years ago. Barley has been used as animal fodder, as a source of fermentable material for beer and certain distilled beverages, and as a component of various health foods. It is used in soups and stews, and in barley bread of various cultures. Barley grains are commonly made into malt in a traditional and ancient method of preparation.",
                    Price = 1.6M
                },
                new Food
                {
                    Name = "Beef",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Meat"),
                    ImageUrl = "https://images.pexels.com/photos/618775/pexels-photo-618775.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription = "Beef is the culinary name for meat from bovines, especially cattle.",
                    LongDescription = "Beef can be harvested from cows, bulls, heifers or steers. Acceptability as a food source varies in different parts of the world.",
                    Price = 8.8M
                },
                new Food
                {
                    Name = "Chicken",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Meat"),
                    ImageUrl = "https://images.pexels.com/photos/616353/pexels-photo-616353.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription = "Chicken is the most common type of poultry in the world, and was one of the first domesticated animals.",
                    LongDescription = "Chicken is a major worldwide source of meat and eggs for human consumption. It is prepared as food in a wide variety of ways, varying by region and culture. The prevalence of chickens is due to almost the entire chicken being edible, and the ease of raising them.",
                    Price = 5.3M
                },
                new Food
                {
                    Name = "Butter",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Milk"),
                    ImageUrl = "https://images.pexels.com/photos/531334/pexels-photo-531334.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=450",
                    InStock = false,
                    IsPreferedFood = false,
                    ShortDescription = "Butter is a dairy product with high butterfat content which is solid when chilled and at room temperature in some regions, and liquid when warmed.",
                    LongDescription = "It is made by churning fresh or fermented cream or milk to separate the butterfat from the buttermilk. It is generally used as a spread on plain or toasted bread products and a condiment on cooked vegetables, as well as in cooking, such as baking, sauce making, and pan frying. Butter consists of butterfat, milk proteins and water, and often added salt.",
                    Price = 5.0M
                },
                new Food
                {
                    Name = "Cheese",
                    Category = _category.GetAll().FirstOrDefault(c => c.Name == "Milk"),
                    ImageUrl = "https://images.pexels.com/photos/821365/pexels-photo-821365.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                    InStock = true,
                    IsPreferedFood = true,
                    ShortDescription = "Cheese is a dairy product derived from milk that is produced in a wide range of flavors, textures, and forms by coagulation of the milk protein casein.",
                    LongDescription = "It comprises proteins and fat from milk, usually the milk of cows, buffalo, goats, or sheep. During production, the milk is usually acidified, and adding the enzyme rennet causes coagulation. The solids are separated and pressed into final form.",
                    Price = 4.4M
                }
            };
        }
        public IEnumerable<Food> GetAll()
        {
            return _foods;
        }

        public Food GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetFilteredFoods(int id, string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetFoodsByCategoryId(int categoryId)
        {
            return _foods.Where(food => food.Category.Id == categoryId);
        }

        public IEnumerable<Food> GetPreferred()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetPreferred(int count)
        {
            throw new NotImplementedException();
        }
    }
}
