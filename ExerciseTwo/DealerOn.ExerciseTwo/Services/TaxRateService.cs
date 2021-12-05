namespace DealerOn.ExerciseTwo.Services;

public class TaxRateService
{
    private const decimal ImportedTaxRate = 0.05M;
    private const decimal NormalTaxRate = 0.1M;
    private static readonly IList<string> TaxFreeItemsKeyWord = new List<string>
    {
        "chocolate", "pills", "book"
    };

    public decimal ApplyTaxes(string item, bool imported, decimal price)
    {
        var taxPercentage = 0M;
        if (imported)
        {
            taxPercentage += ImportedTaxRate;
        }

        if (!TaxFreeItemsKeyWord.Any(x => item.Contains(x, StringComparison.InvariantCultureIgnoreCase)))
        {
            taxPercentage += NormalTaxRate;
        }

        return taxPercentage > 0 ? RoundTaxFiveUpper(price, taxPercentage) : price;
    }

    private static decimal RoundTaxFiveUpper(decimal price, decimal taxPercentage)
    {
        var tax = price * taxPercentage;
        tax = (int)(tax * 100);
        var lastDigit = int.Parse(tax.ToString().Last().ToString());

        if (lastDigit > 0 && lastDigit < 5)
        {
            tax += Math.Abs(lastDigit - 5);
        }
        else if (lastDigit > 5)
        {
            tax += 10 - lastDigit;
        }

        return price + (tax / 100M);
    }
}
