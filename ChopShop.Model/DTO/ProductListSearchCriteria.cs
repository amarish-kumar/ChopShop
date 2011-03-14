namespace ChopShop.Model.DTO
{
    public class ProductListSearchCriteria
    {
        public string SortBy { get; set; }
        public bool Ascending { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ResultsPerPage { get; set; }
        public bool ShowDeletedProducts { get; set; }
    }
}
