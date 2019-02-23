using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : IDespawnable
{

    public EnvironmentSpawner spawner;


    public void Despawn()
    {
        spawner.Despawn(this);
    }

  
}
