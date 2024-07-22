namespace Slots;

public class Symbol(string displayText, float coefficient, float probability)
{
	public string DisplayText { get; } = displayText;
	public float Coefficient { get; } = coefficient;
	public float Probability { get; } = probability;

	public override string ToString() => DisplayText;
}