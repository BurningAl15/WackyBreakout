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
 
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        skinSize = new Vector2(1f, .25f);
            halfColliderWidth = GetComponent<BoxCollider2D>().size.x;
    }

    //This method is called 50 times per second
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 movePaddle=new Vector2(Input.GetAxis("Horizontal"),0);
        Vector2 process = rgb.position + movePaddle * ConfigurationUtils.PaddleMoveUnitsPerSecond*Time.deltaTime;
        
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
            coll.gameObject.GetComponent<Ball>().Bounce(coll.contacts[0].normal);

            // Vector2 contactPoint = coll.contacts[0].point;
            // Vector2 center = collider.bounds.center;
            //
            // if (isTopCollision(contactPoint, center))
            // {
            //     return;
            // }
            // else
            // {
            //     // calculate new ball direction
            //     // float ballOffsetFromPaddleCenter = transform.position.x -
            //     //                                    coll.transform.position.x;
            //     float ballOffsetFromPaddleCenter = contactPoint.x -
            //                                        coll.transform.position.x;
            //     float normalizedBallOffset = ballOffsetFromPaddleCenter /
            //                                  halfColliderWidth;
            //     float angleOffset = normalizedBallOffset * ConfigurationUtils.BounceAngleHalfRange;
            //     float angle = Mathf.PI / 2 + angleOffset;
            //     Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            //
            //     // tell ball to set direction to new direction
            //     Ball ballScript = coll.gameObject.GetComponent<Ball>();
            //     ballScript.SetDirection(direction);
            // }
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
