using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullSpawner : EnemySpawner<Seagull>
{
    public SeagullFactory factory;

    public override void Despawn(Seagull seagull)
    {
        factory.Recycle(seagull);
    }

    public override Seagull Spawn()
    {
        return factory.Get();
    }

    

}
