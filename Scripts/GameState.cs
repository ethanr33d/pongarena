// Assuming this is the content of GameState.cs
using Godot;
using System;

public partial class GameState : Node
{

   
    [Export]
	public int _playerLives = 3; // Default value, can be changed in the Godot editor
	

    private Player playerOne;
    private Player playerTwo;
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
      GD.Print($"Player One Lives: {playerOne.Lives}, Player Two Lives: {playerTwo.Lives}");
      HUD hud = GetNode<HUD>("../HUD");
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
    //refresh hud
}

   

    //reset game state
    public void Reset()
    {
        setPlayerLives();
    }
   
        

    // Additional methods to manage game state...
}