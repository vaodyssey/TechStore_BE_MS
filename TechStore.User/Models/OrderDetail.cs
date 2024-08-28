namespace TechStore.User.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
    }
}
