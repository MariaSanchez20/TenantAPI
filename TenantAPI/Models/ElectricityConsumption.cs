namespace TenantAPI.Models
{
    public class ElectricityConsumption
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public DateTime Date { get; set; }
        public int QuantityKw { get; set; }
    }

}
