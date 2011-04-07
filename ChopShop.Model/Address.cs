namespace ChopShop.Model
{
    public class Address
    {
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string AddressLine3 { get; set; }
        public virtual string AddressLine4 { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Country { get; set; }
    }
}