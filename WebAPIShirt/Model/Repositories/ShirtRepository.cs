namespace WebAPIShirt.Model.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt {ShirtId = 1, Brand = "MyBrand", Color = "Blue", Gender="Men", Price = 30, Size = 10},
            new Shirt {ShirtId = 2, Brand = "MyBrand", Color = "Black", Gender="Men", Price = 35, Size = 12},
            new Shirt {ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender="Women", Price = 28, Size = 8},
            new Shirt {ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender="Women", Price = 30, Size = 9}
        };

        public static bool ShirtExitst(int id)
        {
            return shirts.Any(s => s.ShirtId == id);
        }


        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(s => s.ShirtId == id);
        }
    }
}
