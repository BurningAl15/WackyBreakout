using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ball Script, should bounce in the screen
/// </summary>
public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb;
    private Vector3 lastFrameVelocity;
    private Timer timer;

    private Coroutine currentCoroutine = null;
    private bool startWorking = false;

    private static int speedMultiplier = 1;
    private static float speedUpDuration = 0;
    private static Timer speedUpTimer;

    private static bool changeSpeed = false;
    private float moveSpeed;
    private float _angle = 0;
   
    
    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.AddTimerFinishedListener(FinishedBall);

        rgb = GetComponent<Rigidbody2D>();
        speedUpTimer = gameObject.AddComponent<Timer>();
            
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(StartBallMovement(1));
    }

    private void Update()
    {
        if (startWorking)
        {
            if (changeSpeed)
            {
                Vector2 direction = rgb.velocity.normalized;
                rgb.velocity = direction * moveSpeed;
                changeSpeed = false;
            }
            
            if (timer.Running)
            {
                lastFrameVelocity = rgb.velocity;
            }
        }
        
    }

    void FinishedBall()
    {
        BallSpawner.ballCounter--;
        AudioManager.Play(AudioClipName.DestroyObj);
        HUDManager._instance.SetBallNumber(BallSpawner.ballCounter);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("End"))
        {
            Bounce(other.contacts[0].normal);
            if(other.gameObject.CompareTag("Ball"))
                AudioManager.Play(AudioClipName.BallHitBall);
            if (other.gameObject.CompareTag("Player"))
            {
                var speed = lastFrameVelocity.magnitude;
                rgb.velocity = (this.transform.position - other.transform.position).normalized* Mathf.Max(speed, moveSpeed) ;
            }
            else
                Bounce(other.contacts[0].normal);
            
            // rgb.velocity = Vector3.Reflect(rgb.velocity, other.contacts[0].normal);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            BallSpawner.ballCounter--;
            HUDManager._instance.SetBallNumber(BallSpawner.ballCounter);
    
            Destroy(gameObject);
        }
    }
    
    public void SetDirection(Vector2 direction)
    {
        var ballSpeed = rgb.velocity.magnitude;
        // rgb.velocity = direction * ConfigurationUtils.BallImpulseForce*Time.deltaTime;;
        rgb.velocity = direction * ballSpeed;
    }

    public void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        // Debug.Log("Out Direction: " + direction);
        rgb.velocity = direction * Mathf.Max(speed, moveSpeed);
    }

    
    public static void SpeedUpDuration(int _speedMultiplier,float _duration)
    {
        speedMultiplier = _speedMultiplier;
        speedUpDuration = _duration;
        changeSpeed = true;
    }
    
    private IEnumerator StartBallMovement(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        rgb.MoveRotation(20);

        // var rand = Random.Range(-1, 2);
        // var direction = new Vector2(rand, -1);
        float angle = Random.Range(-Mathf.PI,0 );

        _angle = angle;
        
        Vector2 direction = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        moveSpeed = speedMultiplier* ConfigurationUtils.BallImpulseForce * Time.fixedDeltaTime;

        rgb.velocity = direction * moveSpeed;
        timer.Duration = ConfigurationUtils.BallLifetime;
        timer.Run();

        startWorking = true;
        currentCoroutine = null;
    }
}