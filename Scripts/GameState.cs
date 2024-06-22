// Assuming this is the content of GameState.cs
using Godot;
using System;

public partial class GameState : Node
{

   
    [Export]
	public int _playerLives = 3; // Default value, can be changed in the Godot editor
    [Signal]
    public delegate void GameStateReadyEventHandler();
	

    private Player playerOne;
    private Player playerTwo;
    private HUD hud;
    private bool gameStateIsReady = false;
    public bool roundOver { get; private set; }
    
    public struct Player
    {
        public string Name;
        public int Lives;

        public Player(string name, int lives)
        {
            Name = name;
            Lives = lives;
        }
    }
    
    public override void _Ready()
    {
      setPlayerLives();
      gameStateIsReady = true;
      CheckAndEmitReady();
      GD.Print($"Player One Lives: {playerOne.Lives}, Player Two Lives: {playerTwo.Lives}");
      hud = GetNode<HUD>("../HUD");
      hud.InitializeHUD();
    }
    /// <summary>
    /// Adjusts the lives of a player based on the goal scored and updates the game state accordingly.
    /// </summary>
    /// <param name="goalNumber">The goal number indicating which player scored.</param>
    /// <remarks>
    /// This method should be called whenever a goal is scored in the game.
    /// It determines which player scored and adjusts the opponent's lives accordingly.
    /// </remarks>
    public void GoalScored(int goalNumber)
    {
        if(goalNumber == 1)AdjustLives(2, 1, false);
        else AdjustLives(1, 1, false);
        
    }

    public void setPlayerLives(){
        playerOne.Lives = _playerLives;
        playerTwo.Lives = _playerLives;
    }
    /// <summary>
    /// Adjusts the number of lives for a specified player.
    /// </summary>
    /// <param name="playerNumber">The player number (1 or 2) whose lives are to be adjusted.</param>
    /// <param name="numLives">The number of lives to adjust. This value is added or subtracted based on the 'increase' parameter.</param>
    /// <param name="increase">If true, the number of lives is increased; if false, it is decreased.</param>
    /// <remarks>
    /// This method updates the player's lives by either increasing or decreasing them based on the 'increase' parameter.
    /// After adjusting lives, the HUD is refreshed to reflect the changes.
    /// </remarks>
    public void AdjustLives(int playerNumber, int numLives, bool increase){
    if(playerNumber == 1){
        playerOne.Lives += increase ? numLives : -numLives;
    }else{
        playerTwo.Lives += increase ? numLives : -numLives;
    }
    RefreshGameState();
    }
    private void RefreshGameState(){
        CheckRoundOver();
        PrintGameState();
        hud.RefreshHUD();
        if(!roundOver) ResetBall(); //reset ball if round is not over
        else Reset(); //reset game state  
    }
    private async void ResetBall()
    {
        Ball ball = GetNode<Ball>("../Ball");
        await ToSignal(GetTree().CreateTimer(1.5), "timeout");
        ball.NewBall();
    }

    public int GetPlayerLives(int playerNumber){
        if(playerNumber == 1) return playerOne.Lives;
        else return playerTwo.Lives;
    }
   private void CheckRoundOver()
   {
        if(playerOne.Lives == 0 || playerTwo.Lives == 0) roundOver = true;
        else roundOver = false;
   }

    //reset game state
    private void Reset()
    {
        setPlayerLives();
        ResetBall();
    }
    private void PrintGameState()
    {
        Console.WriteLine($"Player One Lives: {playerOne.Lives}");
        Console.WriteLine($"Player Two Lives: {playerTwo.Lives}");
        Console.WriteLine($"Round Status: {(roundOver ? "Over" : "Ongoing")}");
    }
    public void CheckAndEmitReady()
    {
        // Your logic to check if GameState is ready
        if (gameStateIsReady) // IsReady() needs to be implemented based on your game's logic
        {
            GD.Print("Emitting GameStateReadyEventHandler signal");
            EmitSignal(nameof(GameStateReadyEventHandler));
        }
    }
}