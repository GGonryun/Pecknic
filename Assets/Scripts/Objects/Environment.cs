using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, IDespawnable
{

    private EnvironmentSpawner spawner;

    public void Spawn(EnvironmentSpawner spawner, Vector3 position)
    {
        this.spawner = spawner;
        this.transform.position = position;
    }

    public void Despawn()
    {
        spawner.Despawn(this);
    }

  
}
