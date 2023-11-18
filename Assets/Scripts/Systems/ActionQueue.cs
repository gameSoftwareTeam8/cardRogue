using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public interface IActionQueue
{
    public void enqueue(Action action, int duration);
    public Task run();
}


public class ActionQueue: IActionQueue
{
    private Queue<(Action action, int duration)> action_queue = new();
    public bool is_empty { get { return action_queue.Count == 0; } }
    
    public void enqueue(Action action, int duration)
    {
        action_queue.Enqueue((action, duration));
    }

    public async Task run()
    {
        while (action_queue.Count > 0) {
            (Action action, int duration) = action_queue.Dequeue();
            action();
            await Task.Delay(duration);
        }
    }
}