using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //global access

    public int gold; //amount of gold player has
    public int waveNumber; //number of the wave that's currently being spawned
    public int escapedEnemies; //amount of enemies that made it to the exit
    public int maxAllowedEscapedEnemies = 5; //number of escaped enemies before you lose the game

    public bool enemySpawningOver; //set to true when all enemies have been spawned and is used to help check if player wins

    public AudioClip gameWinSound; //audio for win
    public AudioClip gameLoseSound; //audio for lose

    private bool gameOver; //set true when game is over, win or lose

    private void Awake()
    {
        Instance = this; //set instance to current script
    }

    private void Update() 
    {
        if (!gameOver && enemySpawningOver) //check if game is over yet, but the enemies have all been spawned
        {   
            //Check if no enemies left, if so win game
            if(EnemyManager.Instance.Enemies.Count == 0) //if no enemies player wins
            {
                OnGameWin();
            }
        }

        //when esc is pressed quit to the title screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToTitleScreen();
        }
    }

    private void OnGameWin() //when the player wins play the win audioclip
    {
        AudioSource.PlayClipAtPoint(gameWinSound, Camera.main.transform.position);
        gameOver = true;
        UI.Manager.Instance.ShowinScreen();
        UI.Manager.Instance.ShowLoseScreen();
    }

    public void QuitToTitleScreen() //return to title screen
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnEnemyEscape() //if the amount of enemies that have escaped surpasses the amount allowed, call OnGameLose
    {
        escapedEnemies++;

        if (escapedEnemies == maxAllowedEscapedEnemies)
        {
            //too many enemies escaped, you lose the game
            OnGameLose();
        }
    }

    private void OnGameLose() //set gameOver to true, destroy a;; enemies that are still on the map and tell waveManager to stop spawning
    {
        gameOver = true;

        AudioSource.PlayClipAtPoint(gameLoseSound, Camera.main.transform.position);
        EnemyManager.Instance.DestroyAllEnemies();
        WaveManager.Instance.StopSpawning();
    }

    public void RetryLevel() //reload the game scene
    {
        SceneManager.LoadScene("Game");
    }
}
