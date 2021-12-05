namespace DealerOn.ExerciseTwo.Entities;

public class OrderItem
{

    public OrderItem(string productName, bool imported, decimal originalPrice, decimal price)
    {
        ProductName = productName.Trim();
        Imported = imported;
        OriginalPrice = originalPrice;
        Price = price;
    }

    public string ProductName { get; }
    public int Quantity { get; set; }
    public decimal Price { get; }
    public bool Imported { get; }
    public decimal OriginalPrice { get; }

    public decimal Total => Quantity * Price;

    public decimal TotalTaxes => Quantity * (Price - OriginalPrice);
}
