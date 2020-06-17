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

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        rgb = GetComponent<Rigidbody2D>();

        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(StartBallMovement(1));
    }

    private void Update()
    {
        if (startWorking)
        {
            if (timer.Running)
            {
                lastFrameVelocity = rgb.velocity;
            }
            else if (timer.Finished)
            {
                BallSpawner.ballCounter--;
                HUDManager._instance.SetBallNumber(BallSpawner.ballCounter);

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("End"))
            Bounce(other.contacts[0].normal);
            
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

    private void OnBecameInvisible()
    {
        // BallSpawner.ballCounter--;
        // print(BallSpawner.ballCounter+ " From Ball");
        // HUDManager._instance.SetBallNumber(BallSpawner.ballCounter);
        //
        // Destroy(gameObject);
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
        rgb.velocity = direction * Mathf.Max(speed, ConfigurationUtils.BallImpulseForce * Time.deltaTime);
    }

    private IEnumerator StartBallMovement(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        rgb.MoveRotation(20);

        var rand = Random.Range(-1, 2);
        var direction = new Vector2(rand, -1);

        rgb.velocity = direction * ConfigurationUtils.BallImpulseForce * Time.deltaTime;
        timer.Duration = ConfigurationUtils.BallLifetime;
        timer.Run();

        startWorking = true;
        currentCoroutine = null;
    }
}