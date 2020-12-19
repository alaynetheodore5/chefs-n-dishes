using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]

            public int ChefId { get; set; }

            [Required(ErrorMessage="First Name is required")]
            public string FirstName {get;set;}

            [Required(ErrorMessage="Last Name is required")]
            public string LastName {get;set;}

            [Required(ErrorMessage="Birthday is required")]
            [DataType(DataType.Date)]
            public DateTime Birthday {get;set;}

            public int Age() 
            {
                int age = DateTime.Now.Year-Birthday.Year;
                return age;
            }

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            // A Chef can create many dishes
            // Not stored in database.
            public List<Dish> CreatedDishes { get; set; }
    }
}