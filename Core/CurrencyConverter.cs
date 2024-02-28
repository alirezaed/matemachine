namespace MateMachine.Core
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private static readonly object _lock = new();
        private Dictionary<string, Dictionary<string, double>> _rates = [];

        public void ClearConfiguration()
        {
            this._rates = [];
        }
        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            lock (_lock)
            {
                var conversionPath = FindConversionPath(fromCurrency, toCurrency) ?? throw new ArgumentException("No conversion path found.");
                var convertedAmount = amount;
                foreach (var step in conversionPath)
                {
                    convertedAmount *= step.Item2;
                }
                return Math.Round(convertedAmount, 2);
            }
        }
        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            lock (_lock)
            {
                _rates = [];
                foreach (var pair in conversionRates)
                {
                    if (!_rates.ContainsKey(pair.Item1))
                    {
                        _rates[pair.Item1] = [];
                    }
                    _rates[pair.Item1][pair.Item2] = pair.Item3;

                    if (!_rates.ContainsKey(pair.Item2))
                    {
                        _rates[pair.Item2] = [];
                    }
                    _rates[pair.Item2][pair.Item1] = 1 / pair.Item3;
                }
            }
        }
        private List<Tuple<string, double>>? FindConversionPath(string currencyFrom, string currencyTo)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<List<Tuple<string, double>>>();
            queue.Enqueue([Tuple.Create(currencyFrom, 1.0)]);

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var currentCurrency = path[^1].Item1;

                if (currentCurrency == currencyTo)
                {
                    return path;
                }

                visited.Add(currentCurrency);

                if (!_rates.ContainsKey(currentCurrency))
                    continue;

                foreach (var neighbor in _rates[currentCurrency])
                {
                    var neighborCurrency = neighbor.Key;
                    var rate = neighbor.Value;

                    if (!visited.Contains(neighborCurrency))
                    {
                        var newPath = new List<Tuple<string, double>>(path)
                        {
                            Tuple.Create(neighborCurrency, rate)
                        };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return null;
        }
    }
}
