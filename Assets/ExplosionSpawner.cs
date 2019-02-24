using Andtech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : Singleton<ExplosionSpawner>
{

    public ExplosionFactory factory;

    void Start()
    {
        factory.Initialize();
    }

    public Explosion CreateExplosion(Vector3 position)
    {
        Explosion exp = factory.Get();

        exp.transform.position = position;

        return exp;
    }

    public void Recycle(Explosion obj)
    {
        factory.Recycle(obj);
    }
}
