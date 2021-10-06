using System;
using System.ComponentModel.DataAnnotations;

namespace DemoSeven.WebApi.Models.Entities
{
    //sealed class - cannot be inherited 
    //internal class - only accessible within the assembly 
    
  public class Todo
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(54)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Done is required")]
        public bool Done { get; set; }

        public string Address { get; set; }  
    
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    }
}