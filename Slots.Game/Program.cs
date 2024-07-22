using Slots.Game;

Console.WriteLine("Starting...");

var game = new Game();
while (game.Tick() != GameState.Exit) {}

Console.WriteLine("Your balance hit 0 -- Game Over!!");