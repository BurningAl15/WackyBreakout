using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBlock : Block
{
    [SerializeField] Color color;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private PickupEffect pickupEffect;
    
    void Start()
    {
        pointsPerBlock = ConfigurationUtils.PickupBlockPoints;
            
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
}
