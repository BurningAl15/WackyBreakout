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
    SpeedUpEvent speedUpEvent=new SpeedUpEvent();
    
    [SerializeField] private GameObject freezeObject;
    
    
    
    void Start()
    {
        pointsPerBlock = ConfigurationUtils.PickupBlockPoints;
        if (pickupEffect == PickupEffect.Freezer)
            effectDuration = ConfigurationUtils.FreezeDuration;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;

        if (pickupEffect == PickupEffect.Freezer)
        {
            EventManager.AddFreezeListener(Effect);
            EventManager.AddFreezeInvoker(this);
        }
        else
        {
            EventManager.AddSpeedUpListener(SpeedUpEffect);
            EventManager.AddSpeedUpInvoker(this);
        }        
    }

    void Effect(float duration)
    {
        if (pickupEffect == PickupEffect.Freezer)
        {
            effectDuration = ConfigurationUtils.FreezeDuration;
            //Instantiate an object
            Paddle.FreezeDuration = effectDuration;
        }
    }
    
    void SpeedUpEffect(int speedMultiplier, float duration)
    {
        if (pickupEffect == PickupEffect.Speedup)
        {
            effectDuration = ConfigurationUtils.FreezeDuration;
            //Instantiate an object
            Paddle.SpeedUpDuration(2,effectDuration);
            Ball.SpeedUpDuration(2,effectDuration);
        }
    }
    
    public void AddFreezerEffectListener(UnityAction<float> handler)
    {
        freezerEvent.AddListener(handler);
    }
    
    public void AddSpeedUpEffectListener(UnityAction<int,float> handler)
    {
        speedUpEvent.AddListener(handler);
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
            else
            {
                speedUpEvent.Invoke(2,effectDuration);
            }
            
            Destroy(this.gameObject);
        }
    }
}
