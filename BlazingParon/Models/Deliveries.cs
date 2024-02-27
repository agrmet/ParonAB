using Microsoft.EntityFrameworkCore;
namespace BlazingParon.Models
{
    public class Deliveries
    {
        public int DeliveryId { get; set; }
        public int SendingWarehouseId { get; set; }
        public int ReceivingWarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}