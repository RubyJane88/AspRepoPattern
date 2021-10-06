using System;
using System.ComponentModel.DataAnnotations;

namespace DemoSeven.WebApi.Models.Dtos
{
    //DTOS should be public but immutable and sealed bec. they're part of Web Api endpoints/
    //Don't create descendants of Dtos. 
    //Dtos are properties only and zero methods 
    public sealed class TodoDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public bool Done { get; set; }

        public string Address { get; set; }

  
        
    }
}