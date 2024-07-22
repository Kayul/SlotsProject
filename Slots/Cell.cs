namespace Slots;

public class Cell(Symbol defaultCell)
{
	public Symbol Symbol { get; set; } = defaultCell;

	public void SetRandomSymbol(List<Symbol> availableSymbols)
	{
		Random random = new();

		// Normalize the rolls
		double totalChance = availableSymbols.Sum(kvp => kvp.Probability);
        List<(Symbol Type, double Chance)> normalizedRolls = availableSymbols.Select(kvp => (kvp, kvp.Probability / totalChance)).ToList();
		double roll = random.NextDouble();
		var cumulativeChance = 0.0d;

		foreach (var (Type, Chance) in normalizedRolls)
		{
			cumulativeChance += Chance;
			if (cumulativeChance >= roll)
			{
				Symbol = Type;
				return;
			}
		}

		throw new Exception("RNG Roll failed");
	}

	public override string ToString() => Symbol?.ToString() ?? throw new Exception("Symbol cannot be null.");
}