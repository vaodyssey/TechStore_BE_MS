namespace TechStore.User.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
