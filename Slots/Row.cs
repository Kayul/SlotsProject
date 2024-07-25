namespace Slots;

public class Row(int length = 3)
{
	public List<Cell> Cells = Enumerable
		.Range(1, length)
		.Select(index => new Cell(new("X", 0, 0)))
		.ToList();

	public float TotalCoefficient => Cells.Sum(cell => cell.Symbol.Coefficient);
	
	public void RandomizeRow(List<Symbol> availableSymbols) =>
		Cells.ForEach(cell => cell.SetRandomSymbol(availableSymbols));

	public bool IsWinningRow()
	{
		string? firstSymbol = Cells.FirstOrDefault(cell => cell.Symbol.DisplayText != "*")?.Symbol.DisplayText;
		
		if (firstSymbol == null)
		{
			return true;
		}

		return Cells.All(cell => cell.Symbol.DisplayText == "*" || cell.Symbol.DisplayText == firstSymbol);
	}
	
	public override string ToString() => string.Join("", Cells.Select(cell => cell.ToString()));
}
