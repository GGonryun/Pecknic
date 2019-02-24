using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullSpawner : Spawner<Seagull>
{
    public SeagullFactory factory;
    private bool ready = false;
    private VectorRange movementSpeedRange;
    private VectorRange feedngSpeedRange;


    public void Initialize(VectorRange movementSpeedRange, VectorRange feedingSpeedRange)
    {
        factory.Initialize();
        this.movementSpeedRange = movementSpeedRange;
        this.feedngSpeedRange = feedingSpeedRange;
        ready = true;
    }

    public override void Despawn(Seagull seagull)
    {
        factory.Recycle(seagull);
    }

    public override Seagull Spawn()
    {
        if(!ready)
        {
            throw new System.Exception("Uninitialized SeagullSpawner");
        }

        Seagull gull = factory.Get();
        float speed = Random.Range(movementSpeedRange.min, movementSpeedRange.max);
        float cooldown = Random.Range(feedngSpeedRange.min, feedngSpeedRange.max);
        gull.Spawn(this, speed, cooldown);
        gull.gameObject.SetActive(true);
        gull.StartCoroutine(gull.Speak());
        return gull;
    }

    

}
