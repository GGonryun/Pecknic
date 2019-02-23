using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCollisionEventHandler(object sender, OnCollisionEventArgs e);

public class OnCollisionEventArgs : EventArgs
{
    public Collider Collider { get; private set; }
    public OnCollisionEventArgs(Collider collider)
    {
        this.Collider = collider;
    }
}

public class CollisionSystem : MonoBehaviour
{

    public string[] notifiers;
    private OnCollisionEventHandler collided;

    public event OnCollisionEventHandler Collided
    {
        add
        {
            collided += value;
        }
        remove
        {
            collided -= value;
        }
    }
    private void OnCollided(OnCollisionEventArgs e)
    {
        collided?.Invoke(this, e);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (CollidedWithEnemies(collider))
        {
            OnCollided(new OnCollisionEventArgs(collider));
        }
    }
    private bool CollidedWithEnemies(Collider collision)
    {

        foreach (string notifier in notifiers)
        {
            if (collision.gameObject.CompareTag(notifier))
            {
                return true;
            }
        }
        return false;
    }
}
