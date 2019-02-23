using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour, IDespawnable
{

    private BreadSpawner spawner;

    public void Spawn(BreadSpawner spawner)
    {
        this.spawner = spawner;
    }

    void IDespawnable.Despawn()
    {
        spawner.Despawn(this);
    }
}
