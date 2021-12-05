using System.Globalization;
using System.Text.RegularExpressions;

namespace DealerOn.ExerciseTwo.Services;

public class InputTranslator
{
    private readonly Regex _itemRegex;

    public InputTranslator()
    {
        _itemRegex = new Regex("(\\d+)|(at (\\d+(.\\d+)?))");
    }

    public (string ItemName, int Quantity, decimal Price) Translate(string itemInput)
    {
        var itemName = _itemRegex.Replace(itemInput, string.Empty).Trim();
        var matches = _itemRegex.Matches(itemInput);
        var quantity = int.Parse(matches.First().Value);
        var price = decimal.Parse(matches.Last().Value.Replace("at", "").Trim(), CultureInfo.InvariantCulture);

        return (itemName, quantity, price);
    }
}
