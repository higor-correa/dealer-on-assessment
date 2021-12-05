using DealerOn.ExerciseTwo.Entities;

namespace DealerOn.ExerciseTwo.Services;

public class OrderCoordinator
{
    private readonly Order _order;
    private readonly InputTranslator _inputTranslator;

    public OrderCoordinator()
    {
        _order = new Order();
        _inputTranslator = new InputTranslator();
    }

    public void AddItem(string itemInput)
    {
        var (itemName, quantity, price) = _inputTranslator.Translate(itemInput);
        _order.AddItem(itemName, quantity, price);
    }

    public IList<string> PrintReceipt()
    {
        var lines = _order.Items.Select(x => $"{x.ProductName}: {x.Total:0.00}{DescribePricePerUnit(x)}").ToList();
        lines.Add($"Sales Taxes: {_order.Taxes:0.00}");
        lines.Add($"Total: {_order.Total:0.00}");

        return lines;
    }

    private static string DescribePricePerUnit(OrderItem x)
    {
        return x.Quantity > 1 ? $"({x.Quantity} @ {x.Price:0.00})" : string.Empty;
    }
}
