namespace Slots;

public class Machine(List<Symbol> availableSymbols, int rows = 4, int rowLength = 3)
{
	private List<Symbol> _availableSymbols = availableSymbols;
	private List<Row> _rows = Enumerable
		.Range(1, rows)
		.Select(index => new Row(rowLength))
		.ToList();

	public bool IsPayout => _rows.Any(row => row.IsWinningRow());
	
	public void Spin()
	{
		_rows.ForEach(row => row.RandomizeRow(_availableSymbols));
	}

	public float CalculateWinCoefficient() => _rows
		.Where(row => row.IsWinningRow())
		.Sum(row => row.TotalCoefficient);

	public override string ToString() => string.Join("\n", _rows.Select(cell => cell.ToString()));
}