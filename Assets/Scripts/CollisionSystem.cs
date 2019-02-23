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

    public string[] enemies;
    private OnCollisionEventHandler collided;
    public event OnCollisionEventHandler Collided {
        add {
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
        if(CollidedWithEnemies(collision))
        {
            OnCollided(new OnCollisionEventArgs(collision));
        }
    }

    private bool CollidedWithEnemies(Collision collision)
    {

        foreach (string enemy in enemies)
        {
            if(collision.gameObject.CompareTag(enemy))
            {
                return true;
            }
        }
        return false;
    }
}
