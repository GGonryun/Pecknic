using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        StartCoroutine(TimedExplosion(exp.main.duration));

    }

    IEnumerator TimedExplosion(float duration)
    {
        yield return new WaitForSeconds(duration);
        ExplosionSpawner.Current.Recycle(this);
    }
}
