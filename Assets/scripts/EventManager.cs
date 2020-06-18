using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static List<PickupBlock> invokers = new List<PickupBlock> ();
    static List<UnityAction<float>> listeners = new List<UnityAction<float>> ();

    
    public static void AddInvoker(PickupBlock invoker)
    {
        // add invoker to list and add all listeners to invoker
        invokers.Add(invoker);
        foreach (UnityAction<float> listener in listeners)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }
    
    public static void AddListener(UnityAction<float> handler)
    {		
        // add listener to list and to all invokers
        listeners.Add(handler);
        foreach (PickupBlock teddyBear in invokers)
        {
            teddyBear.AddFreezerEffectListener(handler);
        }
    }
}
