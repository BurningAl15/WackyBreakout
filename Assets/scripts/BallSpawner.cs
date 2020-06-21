using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnLocationMin;
    [SerializeField] Vector2 spawnLocationMax;
    [SerializeField] private GameObject prefabBall;

    private Timer ballSpawnTimer;
    public static int ballCounter = 0;

    private HudReduceBallsLeft reduceBallsLeft=new HudReduceBallsLeft();
    void Start()
    {
        EventManager.AddReduceBallsLEftInvoker(this);
        EventManager.AddReduceBallsLEftListener(HUDManager._instance.ReduceBallsLeft);

        ballSpawnTimer = gameObject.AddComponent<Timer>();
        ballSpawnTimer.AddTimerFinishedListener(ResetTimer);

        InitializeBallSpawn();
        ResetTimer();
    }
    
    void Update()
    {
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
        Instantiate(prefabBall);
        AudioManager.Play(AudioClipName.Spawn);
        ballCounter++;
        // HUDManager._instance.ReduceBallsLeft();
        reduceBallsLeft.Invoke();

        HUDManager._instance.SetBallNumber(ballCounter);
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
    
    public void AddReduceBallsLeftListener(UnityAction handler)
    {
        reduceBallsLeft.AddListener(handler);
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
