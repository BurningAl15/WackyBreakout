﻿using System;
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
        SetScore(0);
        SetBallNumber(0);
    }

    public void SetScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score;
    }

    public void SetBallNumber(int ballNumber)
    {
        ballNumberText.text = "Balls remaining: " + ballNumber;
    }
    
    public void SetBallsPerGame()
    {
        ballsPerGame--;
        ballsPerGameText.text = "Balls in Game: " + ballsPerGame;
    }
}