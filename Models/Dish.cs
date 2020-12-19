using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]

        public int DishId { get; set; }

        [Required(ErrorMessage="Must name the Dish!")]
        [MinLength(2)]
        public string DishName { get; set; }

        [Required(ErrorMessage="Must disclose calories!")]
        public int Calories { get; set; }

        [Required(ErrorMessage="Must choose tastiness level!")]
        public int Tastiness { get; set; }

        [Required(ErrorMessage="Must write a description!")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // This is the foreign key
        public int ChefId { get; set; }

        // A dish can have only one Chef that creates it.
        // Not stored in database.
        public Chef Creater { get; set; }
    }
}