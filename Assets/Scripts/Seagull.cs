using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour, IDespawnable
{
    public GameObject target;
    private float speed;
    private CollisionSystem collisionSystem;
    private bool hasBread = false;
    private float cooldown;
    private SeagullSpawner spawner;

    private string home = "Home";
    private string food = "Picnic";



    public void Spawn(SeagullSpawner spawner, float speed, float cooldown)
    {
        AssignTarget("Home");
        this.speed = speed;
        this.cooldown = cooldown;
        this.spawner = spawner;
        collisionSystem = GetComponent<CollisionSystem>();
        collisionSystem.Collided += ChangeDirection;
        StartCoroutine(Speak());
    }

    private void ChangeDirection(object sender, OnCollisionEventArgs e)
    {
        StartCoroutine(ChangeDirectionDelay(e));
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime * 10f;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 0.1f);
        }
    }

    IEnumerator Speak()
    {
        while(true)
        {

        }
    }

    void IDespawnable.Despawn()
    {
        StopAllCoroutines();

        Explosion exp = ExplosionSpawner.Current.CreateExplosion(this.transform.position);
        exp.Explode();

        target = null;
        spawner.Despawn(this);
    }

    private void AssignTarget(string target)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(target);
        int index = UnityEngine.Random.Range(0, targets.Length);
        this.target = targets[index];
    }

    private IEnumerator ChangeDirectionDelay(OnCollisionEventArgs e)
    {
        target = null;
        if (e.Collider.gameObject.CompareTag(home))
        {
            yield return new WaitForSeconds(cooldown);
            AssignTarget(food);
            if(hasBread)
            {
                hasBread = false;
                GameManager.Current.DecreaseLifePoints();
            }

        }
        else if (e.Collider.gameObject.CompareTag(food))
        {
            yield return new WaitForSeconds(cooldown);
            AssignTarget(home);
            hasBread = true;

        }
    }

}
