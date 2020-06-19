using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] bool retrySpawn = false; 
    [SerializeField] Vector2 spawnLocationMin;
    [SerializeField] Vector2 spawnLocationMax;
    [SerializeField] private GameObject prefabBall;

    private Timer ballSpawnTimer;
    public static int ballCounter = 0;

    void Start()
    {
        ballSpawnTimer = gameObject.AddComponent<Timer>();
        InitializeBallSpawn();   
        
        ResetTimer();
    }
    
    void Update()
    {
        // if (retrySpawn)
        if (ballSpawnTimer.Finished)
        {
            // if (retrySpawn)
            // {
                ResetTimer();
            // }
        }
        
        if(ballCounter < 1)
        {
            SpawnBall();
        }
    }

    void ResetTimer()
    {
        SpawnBall();
        ballSpawnTimer.Duration=Random.Range(ConfigurationUtils.MinBallSpawnTime, ConfigurationUtils.MaxBallSpawnTime+1);
        ballSpawnTimer.Run();
        print("Reseting timer");
    }
    
    void SpawnBall()
    {
        // make sure we don't spawn into a collision
        // if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        // {
        //     retrySpawn = false;
            Instantiate(prefabBall);
            AudioManager.Play(AudioClipName.Spawn);
            ballCounter++;
            HUDManager._instance.SetBallsPerGame();
            HUDManager._instance.SetBallNumber(ballCounter);
        // }
        // else
        // {
        //     retrySpawn = true;
        // }
    }
    
    void InitializeBallSpawn()
    {
        GameObject tempBall = Instantiate<GameObject>(prefabBall);
        
        float ballColliderHalfWidth = 0;
        float ballColliderHalfHeight = 0;
        if (tempBall.GetComponent<BoxCollider2D>() != null)
        {
            BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
            
            ballColliderHalfWidth = collider.size.x / 2;
            ballColliderHalfHeight = collider.size.y / 2;
        }
        else
        {
            CircleCollider2D collider = tempBall.GetComponent<CircleCollider2D>();
            ballColliderHalfWidth = collider.radius;
            ballColliderHalfHeight = collider.radius;
        }        
        
        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);
        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);
        Destroy(tempBall);
    }
    
    void Spawn_Ball(){
        Instantiate(prefabBall);
        ballCounter++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=new Color(1,0,0,.5f);
        Gizmos.DrawLine(spawnLocationMin,new Vector3(spawnLocationMax.x,spawnLocationMin.y));
        Gizmos.DrawLine(new Vector3(spawnLocationMax.x,spawnLocationMin.y),spawnLocationMax);
        Gizmos.DrawLine(new Vector3(spawnLocationMin.x,spawnLocationMax.y),spawnLocationMax);
        Gizmos.DrawLine(new Vector3(spawnLocationMin.x,spawnLocationMax.y),spawnLocationMin);
    }
}
