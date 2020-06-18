using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    [SerializeField] List<Color> colors= new List<Color>();
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        pointsPerBlock = ConfigurationUtils.StandardBlockPoints;
            
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[Random.Range(0, colors.Count)];
    }
}
