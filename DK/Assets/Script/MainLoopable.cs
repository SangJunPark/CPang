using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MainLoopable : ILoopable
{
    private static MainLoopable _instance;

    private List<ILoopable> registeredLoops = new List<ILoopable>();

    public static void Instantiate()
    {
        if (_instance == null)
            _instance = new MainLoopable();

        Logger.Instantiate();
        World.Instantiate();
    }

    public void RegisterLoop(ILoopable loop)
    {
        registeredLoops.Add(loop);
    }

    public void UnregisterLoop(ILoopable loop)
    {
        registeredLoops.Remove(loop);
    }


    public static MainLoopable GetInstance()
    {
        return _instance;
    }
    
    public void Start()
    {
        foreach (ILoopable loop in registeredLoops)
            loop.Start();
    }

    public void Update()
    {
        foreach (ILoopable loop in registeredLoops)
            loop.Update();
    }

    public void OnApplicationQuit()
    {
        foreach (ILoopable loop in registeredLoops)
            loop.OnApplicationQuit();
    }
}
