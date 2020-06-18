using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    private const string ConfigurationDataFileName = "Configuration_Data.csv";

    // configuration data
    private static float paddleMoveUnitsPerSecond = 10;
    private static float ballImpulseForce = 200;
    private static float ballLifetime = 10;
    private static float minBallSpawnTime = 5;
    private static float maxBallSpawnTime = 10;
    private static int standardBlockPoints = 10;
    private static int bonusBlockPoints = 20;
    private static int pickupBlockPoints = 30;

    private static float standardProbabilities = .25f;
    private static float bonusProbabilities = .5f;
    private static float freezeProbabilities = .75f;
    private static float speedUpProbabilities = 1f;

    private static int ballsPerGame = 30;

    private static float freezeDuration = 1f;
    
    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }
    }

    public float BallLifetime=> ballLifetime;
  
    public float MinBallSpawnTime=> minBallSpawnTime;

    public float MaxBallSpawnTime=> maxBallSpawnTime;

    public int StandardBlockPoints => standardBlockPoints;

    public int BonusBlockPoints => bonusBlockPoints;

    public int PickupsBlockPoints => pickupBlockPoints;
    
    public float StandardProbabilities => standardProbabilities;

    public float BonusProbabilities => bonusProbabilities;

    public float FreezeProbabilities => freezeProbabilities;

    public float SpeedUpProbabilities => speedUpProbabilities;

    public int BallsPerGame => ballsPerGame;

    public float FreezeDuration => freezeDuration;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        // read and save configuration data from file
        StreamReader input = null;
        Debug.Log(Path.Combine(
            Application.streamingAssetsPath, ConfigurationDataFileName));
        try
        {
            // create stream reader object
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // read in names and values
            string names = input.ReadLine();
            string values = input.ReadLine();
            // set configuration data fields
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            // always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }

    static void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        // Debug.Log("Paddle Move Units per second: "+paddleMoveUnitsPerSecond + " - From ConfigurationData");
        ballImpulseForce = float.Parse(values[1]);
        // Debug.Log("Ball Impulse Force: "+ballImpulseForce + " - From ConfigurationData");
        ballLifetime = float.Parse(values[2]);
        // Debug.Log( "Ball Lifetime: "+ ballLifetime + " - From ConfigurationData");
        minBallSpawnTime = float.Parse(values[3]);
        // Debug.Log("Min spawn time: "+minBallSpawnTime + " - From ConfigurationData");
        maxBallSpawnTime = float.Parse(values[4]);
        // Debug.Log("Max spawn time: "+maxBallSpawnTime + " - From ConfigurationData");
        standardBlockPoints = int.Parse(values[5]);
        bonusBlockPoints = int.Parse(values[6]);
        pickupBlockPoints = int.Parse(values[7]);

        standardProbabilities = float.Parse(values[8]);
        bonusProbabilities = float.Parse(values[9]);
        freezeProbabilities = float.Parse(values[10]);
        speedUpProbabilities = float.Parse(values[11]);
        ballsPerGame = int.Parse(values[12]);
        freezeDuration = float.Parse(values[13]);
    }

    #endregion
}
