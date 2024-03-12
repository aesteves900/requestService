namespace ProductAndRequests.Models
{
    public record Order(int Id, int? ProductId, int Quantity, decimal TotalPrice);
}