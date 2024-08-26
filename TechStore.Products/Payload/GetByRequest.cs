using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Products.Payload
{    
    public class GetByRequest
    {
        [FromQuery(Name = "searchTerm")]
        public string SearchTerm { get; set; }
        
        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; }
        
        [FromQuery(Name = "sortOrder")]
        public string SortOrder{ get; set; }
        
        [FromQuery(Name = "minPrice")]
        public int MinPrice { get; set; }
        
        [FromQuery(Name = "maxPrice")]
        public int MaxPrice{ get; set; }

        [FromQuery(Name = "pageNumber")]        
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "searchTerm")]        
        public int PageSize { get; set; } = 5;

        [FromQuery(Name = "label")]
        public string  Label { get; set; }
    }
    
}

