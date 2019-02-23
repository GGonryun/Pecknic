using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCollisionEventHandler(object sender, OnCollisionEventArgs e);

public class OnCollisionEventArgs : EventArgs
{
    public Collision Collision { get; private set; }
    public OnCollisionEventArgs(Collision collision)
    {
        this.Collision = collision;
    }
}

public class CollisionSystem : MonoBehaviour
{
    private OnCollisionEventHandler collided;
    public event OnCollisionEventHandler Collided {
        add {
            Debug.Log("New Event");
            collided += value;
        }
        remove {
            collided -= value;
        }
    }


    private void OnCollided(OnCollisionEventArgs e)
    {
        collided?.Invoke(this, e);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollided(new OnCollisionEventArgs(collision));
    }
}
