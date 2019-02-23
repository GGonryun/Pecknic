using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EaseFunctionType { NoEase, EaseIn, EaseOut, EaseExponential, SmoothStep, SmootherStep };

public delegate float EaseFunctionDelegate(float t);

public static class EaseFunction
{
    public static float Calculate(this EaseFunctionType easeFunction, float t)
    {
        switch (easeFunction)
        {
            case EaseFunctionType.NoEase:
                return NoEase(t);
            case EaseFunctionType.EaseIn:
                return EaseIn(t);
            case EaseFunctionType.EaseOut:
                return EaseOut(t);
            case EaseFunctionType.EaseExponential:
                return EaseExponential(t);
            case EaseFunctionType.SmoothStep:
                return SmoothStep(t);
            case EaseFunctionType.SmootherStep:
                return SmootherStep(t);
        }
        return -1;
    }

    private static float NoEase(float t)
    {
        return t;
    }

    private static float EaseIn(float t)
    {
        return (1f - Mathf.Cos(t * Mathf.PI)) * 0.5f;
    }

    private static float EaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI/2f * 0.5f);
    }

    private static float EaseExponential(float t)
    {
        float f = Mathf.Sin(t * Mathf.PI / 2f * 0.5f);
        return Mathf.Pow(f,2);
    }

    private static float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }

    private static float SmootherStep(float t)
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }
}

public class ArmMotor : MonoBehaviour
{
    private bool ready = true;
    private bool dangerous = false;
    private bool contact = false;
    private Vector3 initialPos = Vector3.zero;
    [SerializeField] private EaseFunctionType easeFunctionSelection = EaseFunctionType.NoEase;
    [SerializeField] private float travelDistance = 2f;
    [SerializeField] private float travelDuration = 2f;
    public CollisionSystem collisionSystem;


    void Awake()
    {
        initialPos = transform.localPosition;

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
