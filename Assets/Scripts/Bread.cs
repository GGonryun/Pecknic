using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour, IDespawnable
{

    private BreadSpawner spawner;

    public void Spawn(BreadSpawner spawner, Vector3 pos)
    {
        this.spawner = spawner;
        transform.localPosition = pos;

    }

    void IDespawnable.Despawn()
    {
        spawner.Despawn(this);
    }
}
