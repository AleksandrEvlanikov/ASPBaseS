namespace ASPBase.Models
{
    public class ProductStorege
    {
        public int? ProductId { get; set; }
        public int? StorageId { get; set; }
        public virtual Product? Product { get; set; } 

        public virtual Storage? storage { get; set; } 
    }
}
