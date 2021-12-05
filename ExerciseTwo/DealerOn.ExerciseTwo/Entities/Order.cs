using DealerOn.ExerciseTwo.Services;

namespace DealerOn.ExerciseTwo.Entities;

public class Order
{
    private const string ImportedItemIdentifier = "imported";

    private readonly IList<OrderItem> _items;
    private readonly TaxRateService _taxRateService;

    public Order()
    {
        _items = new List<OrderItem>();
        _taxRateService = new TaxRateService();
    }

    public IReadOnlyList<OrderItem> Items => _items.ToList().AsReadOnly();
    public decimal Taxes => Items.Sum(x => x.TotalTaxes);
    public decimal Total => Items.Sum(x => x.Total);

    public void AddItem(string item, int quantity, decimal price)
    {
        item = item.Trim();

        var orderItem = Items.FirstOrDefault(x => x.ProductName.Equals(item, StringComparison.InvariantCultureIgnoreCase) && price == x.OriginalPrice);
        if (orderItem == null)
        {
            var imported = item.Contains(ImportedItemIdentifier, StringComparison.InvariantCultureIgnoreCase);
            orderItem = new OrderItem(item, imported, price, _taxRateService.ApplyTaxes(item, imported, price));
            _items.Add(orderItem);
        }

        orderItem.Quantity += quantity;
    }
}
