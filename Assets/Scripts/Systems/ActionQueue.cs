using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActionQueue
{
    public void enqueue(Action action);
    public void run();
}


public class ActionQueue: IActionQueue
{
    private Queue<Action> action_queue = new();

    public void enqueue(Action action)
    {
        action_queue.Enqueue(action);
    }

    public void run()
    {
        if (action_queue.Count > 0)
            action_queue.Dequeue()();
    }
}