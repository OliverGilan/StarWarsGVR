using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls game logic including points and health 
public class GameController : MonoBehaviour {

    public bool isGameOver;
    public Text scoreTxt;
    public Text gameOverTxt;
    public Canvas gameOverCanvas;
    public int health;
    public Text healthText;
    public Text enemyCounter;
    public int maxEnemies;
    public AudioSource music;

    private int _currScore;
    private int _scoreToWin = 20;
    private bool _didWin;

	
	// Update is called once per frame
	void Update () {
        
	}

    public void NewGame()
    {
        ResetGame();
    }

    //When an enemy is destroyed by player's laser
    public void GotOne()
    {
        _currScore++;
        scoreTxt.text = "" + _currScore;
        //Ends game
        if(_currScore >= _scoreToWin)
        {
            GameOver(true);
        }
    }

    //If enemy laser hits player
    public void gotHit()
    {
        health--;
        healthText.text = "Health: " + health;
        if(health == 0)
        {
            GameOver(false);
        }
    }

    //End game
    public void GameOver(bool didWin)
    {
        isGameOver = true;
        _didWin = didWin;
        gameOverTxt.text = (_didWin) ? "You saved the galaxy!" : "The empire caught you!";
        gameOverCanvas.enabled = true;
        
        //Destroy enemies and any explosion prefabs in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] hits = GameObject.FindGameObjectsWithTag("hit");
        foreach (GameObject hit in hits)
        {
            Destroy(hit);
        }
        
        music.Stop();
    }

    //Resets game
    public void ResetGame()
    {
        gameOverCanvas.enabled = false;
        isGameOver = false;
        _currScore = 0;
        health = 10;
        healthText.text = "Health: " + health;
        scoreTxt.text = "--";
        enemyCounter.text = "" + maxEnemies;
        maxEnemies = 0;

        music.Play();

        //Again, clears the environment
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("lasers");
        foreach (GameObject laser in bullets)
        {
            Destroy(laser);
        }

        GameObject[] hits = GameObject.FindGameObjectsWithTag("hit");
        foreach (GameObject hit in hits)
        {
            Destroy(hit);
        }
    }

    void Start ()
    {
        NewGame();
    }

    
}
