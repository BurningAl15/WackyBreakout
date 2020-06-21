using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    [SerializeField] List<Color> colors= new List<Color>();
    private SpriteRenderer spriteRenderer;
    
    protected override void Start()
    {
        base.Start();
        pointsPerBlock = ConfigurationUtils.StandardBlockPoints;
            
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[Random.Range(0, colors.Count)];
    }
    
    // protected override void OnCollisionEnter2D(Collision2D coll)
    // {
    //     base.OnCollisionEnter2D(coll);
    // }
}
