namespace ChopShop.Model
{
    public class Image
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string Reference { get; set; }
        public virtual int Order { get; set; }
    }
}