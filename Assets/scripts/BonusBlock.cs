using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
    [SerializeField] Color color;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pointsPerBlock = ConfigurationUtils.BonusBlockPoints;
            
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    // protected override void OnCollisionEnter2D(Collision2D coll)
    // {
    //     // hudAddPoints.Invoke(this.pointsPerBlock);
    //     base.OnCollisionEnter2D(coll);
    // }
}
