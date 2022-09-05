namespace APIEventosBotucatu.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string Adress { get; set; }
        public decimal Price { get; set; }
    }
}
