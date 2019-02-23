using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, IDespawnable
{

    public EnvironmentSpawner spawner;


    public void Despawn()
    {
        spawner.Despawn(this);
    }

  
}
