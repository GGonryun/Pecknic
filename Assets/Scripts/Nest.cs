using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour, IDespawnable
{
    private NestSpawner spawner;

    public void Spawn(NestSpawner spawner, Vector3 position)
    {
        this.spawner = spawner;
        transform.position = position;
    }

    void IDespawnable.Despawn()
    {
        spawner.Despawn(this);
    }

}
