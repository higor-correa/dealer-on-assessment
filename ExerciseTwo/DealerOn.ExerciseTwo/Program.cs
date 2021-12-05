using DealerOn.ExerciseTwo.Services;

var orderCoordinator = new OrderCoordinator();

var line = Console.ReadLine();
while (!string.IsNullOrWhiteSpace(line))
{
    orderCoordinator.AddItem(line);

    line = Console.ReadLine();
}

var lines = orderCoordinator.PrintReceipt();

foreach (var orderLine in lines)
{
    Console.WriteLine(orderLine);
}