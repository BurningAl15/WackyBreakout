using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PickupBlock : Block
{
    [SerializeField] Color color;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private PickupEffect pickupEffect;
    [SerializeField] private float effectDuration;
    FreezerEffectActivated freezerEvent=new FreezerEffectActivated();
    [SerializeField] private GameObject freezeObject;
    void Start()
    {
        pointsPerBlock = ConfigurationUtils.PickupBlockPoints;
        effectDuration = ConfigurationUtils.FreezeDuration;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
        EventManager.AddListener(Effect);
        EventManager.AddInvoker(this);
    }

    public void Effect(float duration)
    {
        if (pickupEffect == PickupEffect.Freezer)
        {
            effectDuration = ConfigurationUtils.FreezeDuration;
            //Instantiate an object
            Paddle.FreezeDuration = effectDuration;
        }
    }
    
    public void AddFreezerEffectListener(UnityAction<float> handler)
    {
        freezerEvent.AddListener(handler);
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            coll.gameObject.GetComponent<Ball>().Bounce(coll.contacts[0].normal);
            HUDManager._instance.SetScore(pointsPerBlock);
            if (pickupEffect == PickupEffect.Freezer)
            {
                Instantiate(freezeObject, this.transform.position, Quaternion.identity);
                freezerEvent.Invoke(effectDuration);
            }
            
            Destroy(this.gameObject);
        }
    }
}
