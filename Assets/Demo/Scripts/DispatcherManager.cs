using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatcherManager : Singleton<DispatcherManager> {

    public readonly Queue<Action> Queue = new Queue<Action>();

    protected void Add(Action action) {
        lock (Queue) {
            Queue.Enqueue(action);
        }
    }

    protected void Update() {
        lock (Queue) {
            if(Queue.Count > 0) {
                Queue.Dequeue()();
            }
        }
    }
}
