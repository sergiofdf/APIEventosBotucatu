using System.ComponentModel.DataAnnotations;

namespace APIEventosBotucatu.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }
        [Required(ErrorMessage = "O id do evento para reserva é obrigatório.")]
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "O nome do responsável pela reserva é obrigatório.")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "A quantidade de pessoas para a reserva é obrigatória.")]
        public long Quantity { get; set; }
    }
}
