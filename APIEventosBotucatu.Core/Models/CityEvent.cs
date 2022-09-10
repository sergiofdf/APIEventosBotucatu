using System.ComponentModel.DataAnnotations;

namespace APIEventosBotucatu.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "O nome do evento é obrigatório.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "A data do evento é obrigatória.")]
        public DateTime DateHourEvent { get; set; }
        [Required(ErrorMessage = "O local do evento é obrigatório.")]
        public string Local { get; set; }
        public string Adress { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } = true;
    }
}
