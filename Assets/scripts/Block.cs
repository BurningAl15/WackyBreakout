using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected int pointsPerBlock;
    
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.CompareTag("Ball"))
        {
            coll.gameObject.GetComponent<Ball>().Bounce(coll.contacts[0].normal);
            HUDManager._instance.SetScore(pointsPerBlock);

            Destroy(this.gameObject);
        }
    }
}
