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
        Instance = this;
    }

    private void Update()
    {
        if (!gameOver && enemySpawningOver)
        {
            if(EnemyManager.Instance.Enemies.Count == 0)
            {
                OnGameWin();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToTitleScreen();
        }
    }

    private void OnGameWin()
    {
        AudioSource.PlayClipAtPoint(gameWinSound, Camera.main.transform.position);
        gameOver = true;
    }

    public void QuitToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
