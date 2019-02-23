using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour, IDespawnable
{
    public GameObject target;
    [SerializeField] private float speed;
    private CollisionSystem collisionSystem;
    private bool hasBread = false;

    public void Spawn(float speed, Vector3 position)
    {
        AssignTarget("Home");
        this.speed = speed;
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
            transform.LookAt(target.transform);
            transform.Translate(target.transform.position * Time.deltaTime * speed * 0.01f);
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

            yield return new WaitForSeconds(2f);
            AssignTarget("Picnic");
            if(hasBread)
            {
                hasBread = false;
                GameManager.Current.DecreaseLifePoints();
            }

        }
        else if (e.Collider.gameObject.CompareTag("Picnic"))
        {
            yield return new WaitForSeconds(2f);
            AssignTarget("Home");
            hasBread = true;

        }
    }

}
