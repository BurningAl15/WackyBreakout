using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Paddle script (is like a character script for this game)
/// </summary>
public class Paddle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb;
    private float halfColliderWidth;
    private Vector2 skinSize;

    private static bool isFrozen = false;
    private static bool isSpeedUp = false;
    private static int speedMultiplier = 1;
    
    private static float freezeDuration = 0;
    private static float speedUpDuration = 0;

    private static Timer freezeTimer;
    private static Timer speedUpTimer;
    

    public static float FreezeDuration
    {
        set
        {
            isFrozen = true;
            freezeDuration = value;
            freezeTimer.Duration = freezeDuration;
            freezeTimer.Run();      
        }
    }

    public static void SpeedUpDuration(int _speedMultiplier,float _duration)
    {
        isSpeedUp = true;
        speedMultiplier = _speedMultiplier;
        speedUpDuration = _duration;
        if (speedUpTimer.Duration > 0)
        {
            speedUpTimer.AddDurationTime(speedUpDuration);
        }
        else
        {
            speedUpTimer.Duration = speedUpDuration;
        }
        speedUpTimer.Run();
    }

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        
        freezeTimer = gameObject.AddComponent<Timer>();
        freezeTimer.AddTimerFinishedListener(FinishedFreeze);
        speedUpTimer = gameObject.AddComponent<Timer>();
        speedUpTimer.AddTimerFinishedListener(FinishedSpeedUp);
        
        skinSize = new Vector2(1f, .25f);
            halfColliderWidth = GetComponent<BoxCollider2D>().size.x;
            
    }

    void FixedUpdate()
    {
        if (freezeTimer.Running)
        {
            if (speedUpTimer.Duration > 0)
            {
                speedUpTimer.Stop();
            }
        }
        if (speedUpTimer.Running)
        {
            if (freezeTimer.Duration > 0)
            {
                freezeTimer.Stop();
            }
        }

        // if (freezeTimer.Finished)
        // {
        //     FinishedFreeze();
        // }
        // if (speedUpTimer.Finished)
        // {
        //     FinishedSpeedUp();
        // }
        
        if(!isFrozen)
            Move();
    }

    void FinishedFreeze()
    {
        if (speedUpTimer.Duration > 0)
        {
            speedUpTimer.Run();
        }
        isFrozen = false;
    }

    void FinishedSpeedUp()
    {
        if (freezeTimer.Duration > 0)
        {
            freezeTimer.Run();
        }
            
        speedMultiplier = 1;
        Ball.SpeedUpDuration(1,0);
        isSpeedUp = false;
    }

    void Move()
    {
        Vector2 movePaddle=new Vector2(Input.GetAxis("Horizontal"),0);
        Vector2 process = rgb.position + movePaddle * ConfigurationUtils.PaddleMoveUnitsPerSecond * speedMultiplier * Time.deltaTime;
        
        rgb.MovePosition(new Vector2(CalculateClampedX(process.x),process.y));
    }

    float CalculateClampedX(float xPosition)
    {
        return Mathf.Clamp(xPosition, ScreenUtils.ScreenLeft + (halfColliderWidth ),ScreenUtils.ScreenRight-
                                                                                                    (halfColliderWidth));
    }
    
    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        Collider2D collider = coll.collider;
        
        if (coll.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.PaddleHit);
            // coll.gameObject.GetComponent<Ball>().Bounce(coll.contacts[0].normal);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(ScreenUtils.ScreenLeft + (halfColliderWidth),this.transform.position.y,0),Vector3.one*.5f);
        Gizmos.DrawCube(new Vector3(ScreenUtils.ScreenRight - (halfColliderWidth),this.transform.position.y,0),Vector3.one*.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(this.transform.position,new Vector3(skinSize.x,skinSize.y,1));
    }

    bool isTopCollision(Vector2 contactPoint,Vector2 colliderCenter)
    {
        if (contactPoint.y>colliderCenter.y)
            return true;
        return false;
    }
}
