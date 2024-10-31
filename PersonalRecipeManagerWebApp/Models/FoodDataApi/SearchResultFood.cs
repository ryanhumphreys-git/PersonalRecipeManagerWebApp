namespace PersonalRecipeManagerWebApp.Models.FoodDataApi
{
    public class SearchResultFood
    {
        public int fdcId { get; set; }
        public string dataType { get; set; }
        public string description { get; set; }
        public string foodCode { get; set; }
        public FoodNutrients foodNutrients { get; set; }
        public string publicationDate { get; set; }
        public string scientificName { get; set; }
        public string brandOwner { get; set; }
        public string gtinUpc { get; set; }
        public string ingredients { get; set; }
        public int nbdNumber { get; set; }
        public string additionalDescriptions { get; set; }
        public string allHighlightFields { get; set; }
        public double score { get; set; }
    }
}
