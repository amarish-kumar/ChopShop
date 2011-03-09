namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditPrice
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public bool IsTaxIncluded { get; set; }
        public decimal TaxRate { get; set; }
    }
}