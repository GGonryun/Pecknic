﻿using System.Collections;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private bool ready = true;
    private bool dangerous = false;
    [SerializeField] private EaseFunctionType easeFunctionSelection = EaseFunctionType.NoEase;
    [SerializeField] private float travelDistance = 2f;
    [SerializeField] private float travelDuration = 2f;
    [SerializeField] private CollisionSystem collisionSystem;

    public event OnCollisionEventHandler Collided;

    void Awake()
    {
        collisionSystem.Collided += OnCollision;
    }

    public void OnCollision(object sender, OnCollisionEventArgs e)
    {
        if(dangerous)
        {
            Collided?.Invoke(this, e);
        }
    }

    public void Punch()
    {
        if (ready)
        {
            //Debug.Log("B: Punch Ready, Throwing Punch...");
            StartCoroutine(MoveArm());
        }
        else
        {
            //Debug.Log("B: Punch is not ready...");
        }
    }

    public IEnumerator MoveArm()
    {
        //Punch attempted.
        ready = false;

        Vector3 startingPos = transform.localPosition;

        Vector3 newPos = transform.localPosition;
        newPos.z += travelDistance;

        //Forwards action
        dangerous = true;
        yield return ArmAction(startingPos, newPos, travelDuration, easeFunctionSelection);
        dangerous = false;

        //Backwards action
        yield return ArmAction(newPos, startingPos, travelDuration, easeFunctionSelection);
        transform.localPosition = startingPos;

        //Punch completed.
        ready = true;
    }

    public IEnumerator ArmAction(Vector3 startingPos, Vector3 newPos, float maxDuration, EaseFunctionType easeFunction)
    {
        //Begin Arm Movement
        float elapsedTime = -0.1f;
        while (elapsedTime <= maxDuration)
        {
            float ratio = elapsedTime / maxDuration;
            float easedRatio = EaseFunction.Calculate(easeFunctionSelection, ratio);

            transform.localPosition = Vector3.Lerp(startingPos, newPos, easedRatio);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


}
