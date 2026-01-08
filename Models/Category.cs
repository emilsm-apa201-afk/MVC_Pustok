using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MVC_Pustokkk.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }   

        public bool IsDeleted { get; set; }  

        [NotMapped]                         
        public IFormFile? Photo { get; set; } 

        public List<Featured> Featureds { get; set; } = new();
    }
}

