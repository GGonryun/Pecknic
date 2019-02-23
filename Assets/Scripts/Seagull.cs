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

    public void Spawn(float speed, Vector3 position, float cooldown)
    {
        AssignTarget("Home");
        this.speed = speed;
        this.cooldown = cooldown;
        this.transform.position = position;
        collisionSystem = GetComponent<CollisionSystem>();
        collisionSystem.Collided += ChangeDirection;
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

    void IDespawnable.Despawn()
    {
        target = null;
        SeagullSpawner.Current.Despawn(this);
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
        if (e.Collider.gameObject.CompareTag("Home"))
        {
            yield return new WaitForSeconds(cooldown);
            AssignTarget("Picnic");
            if(hasBread)
            {
                hasBread = false;
                GameManager.Current.DecreaseLifePoints();
            }

        }
        else if (e.Collider.gameObject.CompareTag("Picnic"))
        {
            yield return new WaitForSeconds(cooldown);
            AssignTarget("Home");
            hasBread = true;

        }
    }

}
