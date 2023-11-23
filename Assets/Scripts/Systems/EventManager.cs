using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEventManager
{
    public void register(string name, Action action);
    public void remove(string name, Action action);
    public void notify(string name);
}

public class EventManager: IEventManager
{
    Dictionary<string, List<Action>> events = new();
    public void register(string name, Action action)
    {
        if (!events.ContainsKey(name))
            events[name] = new();
        events[name].Add(action);
    }

    public void remove(string name, Action action)
    {
        if (!events.ContainsKey(name))
            return;

        events[name].Remove(action);
    }

    public void notify(string name)
    {
        if (!events.ContainsKey(name))
            return;
        
        foreach (var action in events[name])
            action();
    }
}