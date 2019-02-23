using Andtech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner<TSpawn> : Singleton<EnemySpawner<TSpawn>> where TSpawn : IDespawnable
{
    public abstract TSpawn Spawn();

    public abstract void Despawn(TSpawn seagull);
}
