using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ballNumberText;
    [SerializeField] private Text ballsPerGameText;
    private int score = 0;
    private int ballsPerGame = 0;
    public static HUDManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        ballsPerGame = ConfigurationUtils.BallsPerGame;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = "Score: " + score;
        SetBallNumber(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    public void AddPoints(int _score)
    {
        score += _score;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = "Score: " + score;
    }

    public void SetBallNumber(int ballNumber)
    {
        ballNumberText.text = "Balls remaining: " + ballNumber;
    }
    
    public void ReduceBallsLeft()
    {
        ballsPerGame--;
        if(ballsPerGame<=0)
            MenuManager.GoToMenu(MenuName.EndGame);

        ballsPerGameText.text = "Balls Left: " + ballsPerGame;
    }
}
