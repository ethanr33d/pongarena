// Assuming this is the content of GameState.cs
using Godot;
using System;

public partial class GameState : Node
{

    [Export]
	public int _playerLives = 3; // Default value, can be changed in the Godot editor
    public int _numPlayers = 2; //eh, who knows what could happen, simpler to refer in an  array
   

    private Player playerOne;
    private Player playerTwo;
    private Player[] players;
    private HUD hud;
    private bool gameStateIsReady = false;
    public bool roundOver { get; private set; }
    
    public struct Player
    {
        public string Name;
        public int currentLives;
        public int maxLives;

        public Player(string name, int lives)
        {
            Name = name;
            currentLives = lives;
            maxLives = lives;
        }
    }
    
    public override void _Ready()
    {
      setPlayerLives();

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
        if(goalNumber == 1)AdjustLives(1, 1, false);
        else AdjustLives(2, 1, false);
        
    }

    public void setPlayerLives(){
        playerOne.currentLives = _playerLives;
        playerOne.maxLives = _playerLives;
        playerTwo.currentLives = _playerLives;
        playerTwo.maxLives = _playerLives;
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
        playerOne.currentLives += increase ? numLives : -numLives;
    }else{
        playerTwo.currentLives += increase ? numLives : -numLives;
    }
    RefreshGameState();
    }
    private void RefreshGameState(){
        CheckRoundOver();
       // PrintGameState();
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

    public int GetPlayerCurrentLives(int playerNumber){
        if(playerNumber == 1) return playerOne.currentLives;
        else return playerTwo.currentLives;
    }
    public int GetPlayerMaxLives(){
        return _playerLives; //this is the passed lives from godot menu, max lives
    }
   private void CheckRoundOver()
   {
        if(playerOne.currentLives == 0 || playerTwo.currentLives == 0) roundOver = true;
        else roundOver = false;
   }

    //reset game state
    private void Reset()
    {
        setPlayerLives();
        hud.RefreshHUD();
        ResetBall();
    }
    private void PrintGameState()
    {
        Console.WriteLine($"Player One currentLives: {playerOne.currentLives}");
        Console.WriteLine($"Player Two currentLives: {playerTwo.currentLives}");
        Console.WriteLine($"Round Status: {(roundOver ? "Over" : "Ongoing")}");
    }
   
}