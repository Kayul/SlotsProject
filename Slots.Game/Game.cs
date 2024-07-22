using Slots.Game.Player;

namespace Slots.Game;

// Main game loop
public class Game
{
	private float _activeStake = 0f;
	private readonly Currency _balance = new(0, '£');
	private GameState _gameState = GameState.Deposit;
	private Machine _machine = new([
		new Symbol("A", 0.4f, 0.45f),
		new Symbol("B", 0.6f, 0.35f),
		new Symbol("P", 0.8f, 0.15f),
		new Symbol("*", 0f,   0.05f),
	]);

	public GameState Tick()
	{
		switch (_gameState)
		{
			case GameState.Deposit:
				Deposit();
				break;

			case GameState.Stake:
				Stake();
				break;

			case GameState.Spin:
				Spin();
				break;

			case GameState.Cleanup:
				Cleanup();
				break;
		}

		Console.WriteLine(" ");
		return _gameState;
	}

	private static bool GetUserInput(string messageRequest, out float value)
	{
		Console.WriteLine(messageRequest);
		string? input = Console.ReadLine();
		if (float.TryParse(input, out value))
			return true;
		return false;
	}

	protected virtual void Deposit()
	{
		ShowBalance();
		if (!GetUserInput("Please enter the amount you would like to deposit:", out float depositAmount))
		{
			Console.WriteLine("Please enter a valid number to deposit.");
			return;
		}

		if (depositAmount < 1)
		{
			Console.WriteLine("Please enter a number larger than 1.");
			return;
		}

		_balance.Add(depositAmount);
		_gameState = GameState.Stake;
	}

	protected virtual void Stake()
	{
		ShowBalance();

		if (!GetUserInput("Please enter the amount you would like to stake:", out float stakeAmount))
		{
			Console.WriteLine("Please enter a valid number to stake.");
			return;
		}

		if (!_balance.TryTake(stakeAmount))
		{
			Console.WriteLine("That amount would bring your balance below 0. Please enter a valid amount.");
			return;
			
		}

		_activeStake = stakeAmount;
		_gameState = GameState.Spin;
	}

	protected virtual void Spin()
	{
		_machine.Spin();
		Console.WriteLine(_machine.ToString());

		float winningCoefficient = _machine.CalculateWinCoefficient();
		float winnings = MathF.Round(winningCoefficient * _activeStake, 2);
		_balance.Add(winnings);

		if (_machine.IsPayout)
			Console.WriteLine("You have won: {0}{1}", _balance.Prefix, winnings);

		_gameState = GameState.Cleanup;
	}

	protected virtual void Cleanup()
	{
		if (_balance.Value < 1)
		{
			_gameState = GameState.Exit;
			return;
		}

		_gameState = GameState.Stake;
	}

	private void ShowBalance() => Console.WriteLine("Your balance is currently: {0}", _balance.ToString());
}

public enum GameState
{
	Deposit,
	Stake,
	Spin,
	Cleanup,
	Exit
}