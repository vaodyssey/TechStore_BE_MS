namespace TechStore.Products.Models
{
    public class Product:BaseEntity
    {         
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string Image { get; set; }
        public int Price { get; set; }          
        public DateTime CreatedAt { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
