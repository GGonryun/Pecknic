using Andtech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner<T> : Singleton<EnemySpawner<T>> where T : IDespawnable
{

    public abstract T Spawn();

    public abstract void Despawn(T seagull);
    
}
