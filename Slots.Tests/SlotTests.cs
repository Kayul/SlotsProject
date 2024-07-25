namespace Slots.Tests;

public class SlotTests
{
    [Fact]
    public void SetRandomSymbolTest()
    {
		var symbol = new Symbol("X", 0, 0);
		var cell = new Cell(symbol);
		cell.SetRandomSymbol(TestUtil.Symbols);

		// Hard to test the RNG here, but we can ensure
		// that it will have changed from a previous value
		Assert.NotEqual("X", cell.Symbol.DisplayText);
    }

	[Fact]
	public void RandomizeRow()
	{
		// Same principle as the previous
		var row = new Row();
		row.RandomizeRow(TestUtil.Symbols);

		foreach(var cell in row.Cells)
			Assert.NotEqual("X", cell.Symbol.DisplayText);
	}

	[Fact]
	public void TestWinningRow()
	{
		var winningRow = new Row(3);
		winningRow.Cells[0].Symbol = TestUtil.Symbols[0];
		winningRow.Cells[1].Symbol = TestUtil.Symbols[0];
		winningRow.Cells[2].Symbol = TestUtil.Symbols[0];
		Assert.True(winningRow.IsWinningRow());
		
		var losingRow = new Row(3);
		losingRow.Cells[0].Symbol = TestUtil.Symbols[0];
		losingRow.Cells[1].Symbol = TestUtil.Symbols[2];
		losingRow.Cells[2].Symbol = TestUtil.Symbols[1];
		Assert.False(losingRow.IsWinningRow());
	}

	[Fact]
	public void TestWinningRowWildcard()
	{
		var winningRow = new Row(3);
		winningRow.Cells[0].Symbol = TestUtil.Symbols[0];
		winningRow.Cells[1].Symbol = TestUtil.Symbols[3];
		winningRow.Cells[2].Symbol = TestUtil.Symbols[0];
		Assert.True(winningRow.IsWinningRow());
		
		var losingRow = new Row(3);
		losingRow.Cells[0].Symbol = TestUtil.Symbols[3];
		losingRow.Cells[1].Symbol = TestUtil.Symbols[0];
		losingRow.Cells[2].Symbol = TestUtil.Symbols[1];
		Assert.False(losingRow.IsWinningRow());

		// Checking when wildcard (*) comes first in a winning row
		var wildcardRow = new Row(3);
		wildcardRow.Cells[0].Symbol = TestUtil.Symbols[3];
		wildcardRow.Cells[1].Symbol = TestUtil.Symbols[0];
		wildcardRow.Cells[2].Symbol = TestUtil.Symbols[0];
		Assert.True(wildcardRow.IsWinningRow());

		wildcardRow.Cells[0].Symbol = TestUtil.Symbols[3];
		wildcardRow.Cells[1].Symbol = TestUtil.Symbols[3];
		wildcardRow.Cells[2].Symbol = TestUtil.Symbols[3];
		Assert.True(wildcardRow.IsWinningRow());
	}

	[Fact]
	public void TestTotalCoeff()
	{
		var row = new Row(3);
		row.Cells[0].Symbol = TestUtil.Symbols[0];
		row.Cells[1].Symbol = TestUtil.Symbols[0];
		row.Cells[2].Symbol = TestUtil.Symbols[0];
		Assert.Equal(0.4f + 0.4f + 0.4f, row.TotalCoefficient);

		row.Cells[0].Symbol = TestUtil.Symbols[0];
		row.Cells[1].Symbol = TestUtil.Symbols[1];
		row.Cells[2].Symbol = TestUtil.Symbols[2];
		Assert.Equal(0.8f + 0.6f + 0.4f, row.TotalCoefficient);

		row.Cells[0].Symbol = TestUtil.Symbols[3];
		row.Cells[1].Symbol = TestUtil.Symbols[3];
		row.Cells[2].Symbol = TestUtil.Symbols[3];
		Assert.Equal(0, row.TotalCoefficient);
	}
}

public static class TestUtil
{
	public static List<Symbol> Symbols =>
	[
		new Symbol("A", 0.4f, 0.45f),
		new Symbol("B", 0.6f, 0.35f),
		new Symbol("P", 0.8f, 0.15f),
		new Symbol("*", 0f,   0.05f)
	];
}