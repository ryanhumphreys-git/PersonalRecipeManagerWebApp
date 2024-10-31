namespace PersonalRecipeManagerWebApp.Models.RecipeApi
{
    public class MealsDbSearchCleaned
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Region { get; set; }
        public string Instructions { get; set; }
        public List<string> ingredients { get; set; } = new();
        public List<double> measurement { get; set; } = new();
        public List<string> units { get; set; } = new();
    }
}
