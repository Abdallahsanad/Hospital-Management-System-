using System.ComponentModel.DataAnnotations;

namespace Hospital.Train.PL.ViewModels.Medicine
{
    public class MidicineViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
