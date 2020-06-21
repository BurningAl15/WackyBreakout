using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static PickupBlock freezeInvokers = new PickupBlock();
    static UnityAction<float> freezeListeners;

    static PickupBlock speedUpInvokers= new PickupBlock();
    static UnityAction<int,float> speedUpListeners;
    
    static Block addPointsInvokers=new Block();
    static UnityAction<int> addPointsListeners;
    
    static List<BallSpawner> reduceBallsLeftInvokers=new List<BallSpawner>();
    static List<UnityAction> reduceBallsLeftListeners=new List<UnityAction>();
    
    
    //Solves the problem of running all the events of subscribed elements in a list
    //This runs just one event per call so it's perfect for scoring.
    public static void AddFreezeInvoker(PickupBlock invoker)
    {
        freezeInvokers=invoker;
        invoker.AddFreezerEffectListener(freezeListeners);
    }
    
    public static void AddFreezeListener(UnityAction<float> handler)
    {		
        freezeListeners=handler;
        freezeInvokers.AddFreezerEffectListener(handler);
    }
    
    public static void AddSpeedUpInvoker(PickupBlock invoker)
    {
        speedUpInvokers=invoker;
        invoker.AddSpeedUpEffectListener(speedUpListeners);
    }
    
    public static void AddSpeedUpListener(UnityAction<int,float> handler)
    {		
        speedUpListeners=handler;
        speedUpInvokers.AddSpeedUpEffectListener(handler);
    }

    public static void AddPointsAddedInvoker(Block invoker)
    {
        addPointsInvokers=invoker;
        invoker.AddPointsAddedListener(addPointsListeners);
    }
    
    public static void AddPointsAddedListener (UnityAction<int> handler)
    {		
        addPointsListeners=handler;
        addPointsInvokers.AddPointsAddedListener(handler);
    }

    public static void AddReduceBallsLEftInvoker(BallSpawner invoker)
    {
        // add invoker to list and add all listeners to invoker
        reduceBallsLeftInvokers.Add(invoker);
        foreach (UnityAction listener in reduceBallsLeftListeners)
        {
            invoker.AddReduceBallsLeftListener(listener);
        }
    }
    
    public static void AddReduceBallsLEftListener (UnityAction handler)
    {		
        // add listener to list and to all invokers
        reduceBallsLeftListeners.Add(handler);
        foreach (BallSpawner ball in reduceBallsLeftInvokers)
        {
            ball.AddReduceBallsLeftListener(handler);
        }
    }
}
