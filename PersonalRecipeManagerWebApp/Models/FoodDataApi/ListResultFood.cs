namespace PersonalRecipeManagerWebApp.Models.FoodDataApi
{
    public class ListResultFood
    {
        public int fdcId { get; set; }
        public string description { get; set; }
        public string dataType { get; set; }
        public string publicationDate { get; set; }
        public string brandOwner { get; set; }
        public string gtinUpc { get; set; }
        public List<FoodNutrients> foodNutrients { get; set; }
        //public int nbdNumber { get; set; }
        //public string foodCode { get; set; }

    }
}
