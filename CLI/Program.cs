using MateMachine.Core;

var converter = new CurrencyConverter();
converter.UpdateConfiguration([
    new("USD", "CAD", 1.2),
    new("CAD", "EUR", 1.1),
    new("EUR", "USD", 1.4),
    new("JPY", "USD", 1.11),
]);

Console.WriteLine("From:");
var from = Console.ReadLine();

Console.WriteLine("To:");
var to = Console.ReadLine();

Console.WriteLine("Amount:");
var amount = Console.ReadLine();

if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || !int.TryParse(amount, out int parsedAmount))
    throw new ArgumentException("From and To should not be null and Amount should be able to parse to int");

var result = converter.Convert(from, to, parsedAmount);
Console.WriteLine("Result:" + result);