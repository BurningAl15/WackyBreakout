using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    private static ConfigurationData configurationData;

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get
        {
            return configurationData.PaddleMoveUnitsPerSecond;
        }
    }


    /// <summary>
    /// Ball Impulse force
    /// </summary>
    /// <value>Impulse of the ball</value>
    public static float BallImpulseForce
    {
        get
        {
            return configurationData.BallImpulseForce;
        }
    }

    public static float BallLifetime
    {
        get
        {
            return configurationData.BallLifetime;
        }
    }

    public static float BounceAngleHalfRange
    {
        get { return 60 * Mathf.Rad2Deg; }
    }

    public static float MinBallSpawnTime
    {
        get
        {
            return configurationData.MinBallSpawnTime;
        }
    }
    
    public static float MaxBallSpawnTime
    {
        get
        {
            return configurationData.MaxBallSpawnTime;
        }
    }

    public static int StandardBlockPoints
    {
        get
        {
            return configurationData.StandardBlockPoints;
        }
    }
    
    public static int BonusBlockPoints
    {
        get
        {
            return configurationData.BonusBlockPoints;
        }
    }
    
    public static int PickupBlockPoints
    {
        get
        {
            return configurationData.PickupsBlockPoints;
        }
    }

    public static float StandardProbability
    {
        get
        {
            return configurationData.StandardProbabilities;
        }
    }
    
    public static float BonusProbability
    {
        get
        {
            return configurationData.BonusProbabilities;
        }
    }
    
    public static float FreezeProbabilities
    {
        get
        {
            return configurationData.FreezeProbabilities;
        }
    }
    
    public static float SpeedUpProbability
    {
        get
        {
            return configurationData.SpeedUpProbabilities;
        }
    }
    
    public static int BallsPerGame
    {
        get
        {
            return configurationData.BallsPerGame;
        }
    }

    public static float FreezeDuration
    {
        get
        {
            return configurationData.FreezeDuration;
        }
    }
#endregion

    
    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
