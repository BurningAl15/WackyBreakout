using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static List<PickupBlock> freezeInvokers = new List<PickupBlock> ();
    static List<UnityAction<float>> freezeListeners = new List<UnityAction<float>> ();

    static List<PickupBlock> speedUpInvokers= new List<PickupBlock>();
    static List<UnityAction<int,float>> speedUpListeners=new List<UnityAction<int, float>>();
    
    public static void AddFreezeInvoker(PickupBlock invoker)
    {
        // add invoker to list and add all listeners to invoker
        freezeInvokers.Add(invoker);
        foreach (UnityAction<float> listener in freezeListeners)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }
    
    public static void AddFreezeListener(UnityAction<float> handler)
    {		
        // add listener to list and to all invokers
        freezeListeners.Add(handler);
        foreach (PickupBlock teddyBear in freezeInvokers)
        {
            teddyBear.AddFreezerEffectListener(handler);
        }
    }
    
    public static void AddSpeedUpInvoker(PickupBlock invoker)
    {
        // add invoker to list and add all listeners to invoker
        speedUpInvokers.Add(invoker);
        foreach (UnityAction<int,float> listener in speedUpListeners)
        {
            invoker.AddSpeedUpEffectListener(listener);
        }
    }
    
    public static void AddSpeedUpListener(UnityAction<int,float> handler)
    {		
        // add listener to list and to all invokers
        speedUpListeners.Add(handler);
        foreach (PickupBlock ball in speedUpInvokers)
        {
            ball.AddSpeedUpEffectListener(handler);
        }
    }
    
}
