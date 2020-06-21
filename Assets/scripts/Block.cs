using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] protected int pointsPerBlock;
    protected HudAddPoints hudAddPoints=new HudAddPoints();

    protected virtual void Start()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            EventManager.AddPointsAddedListener(HUDManager._instance.AddPoints);
            EventManager.AddPointsAddedInvoker(this);
            AudioManager.Play(AudioClipName.HitSound);
            LevelBuilder.blocksInGame--;
            hudAddPoints.Invoke(this.pointsPerBlock);
            Destroy(this.gameObject);
        }
    }
    
    public void AddPointsAddedListener(UnityAction<int> handler)
    {
        hudAddPoints.AddListener(handler);
    }

    // private void OnDestroy()
    // {
    //     throw new NotImplementedException();
    // }
}
