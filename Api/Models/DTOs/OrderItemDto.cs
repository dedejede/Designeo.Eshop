namespace Designeo.Eshop.Api.Models.DTOs
{
    public class OrderItemDto
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}