using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Products.Payload
{    
    public class GetByRequest
    {                       
        public string SearchTerm { get; set; }        
        public string SortBy { get; set; }
        public string SortOrder{ get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice{ get; set; }
        public int PageNumber{ get; set; }
        public int PageSize { get; set; }
        public string  Label { get; set; }
    }
    
}

