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
    
    
    
    protected override void Start()
    {
        base.Start();
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

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            EventManager.AddPointsAddedListener(HUDManager._instance.AddPoints);
            EventManager.AddPointsAddedInvoker(this);
            coll.gameObject.GetComponent<Ball>().Bounce(coll.contacts[0].normal);
            // HUDManager._instance.AddPoints(pointsPerBlock);
            
            if (pickupEffect == PickupEffect.Freezer)
            {
                Instantiate(freezeObject, this.transform.position, Quaternion.identity);
                freezerEvent.Invoke(effectDuration);
                AudioManager.Play(AudioClipName.PickupFreeze);
            }
            else
            {
                speedUpEvent.Invoke(2,effectDuration);
                AudioManager.Play(AudioClipName.PickupSpeedUp);
            }
            LevelBuilder.blocksInGame--;
            hudAddPoints.Invoke(this.pointsPerBlock);
            Destroy(this.gameObject);
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
}
