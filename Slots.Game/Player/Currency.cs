namespace Slots.Game.Player;

public class Currency(float initial, char prefix)
{
	public float Value { get; private set; } = initial;
	public char Prefix { get; private set; } = prefix;

	public bool TryTake(float amount)
	{
		if (Value - amount < 0)
		{
			return false;
		}
		
		Value -= amount;
		return true;
	}

	public void Add(float amount) =>
		Value += amount;

	public override string ToString() =>
		$"{Prefix}{Math.Round(Value, 2)}";
}