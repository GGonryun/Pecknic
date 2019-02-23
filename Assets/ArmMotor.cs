using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EaseFunctionType { EaseIn, EaseOut };

public delegate float EaseFunction(float initialTime, float maximumTime);

public static class EaseFunctions {

    public static float Calculate(this EaseFunctionType easeFunction, float initialTime, float maximumTime)
    {
        switch(easeFunction)
        {
            case EaseFunctionType.EaseIn:
            return EaseIn(initialTime, maximumTime);

            case EaseFunctionType.EaseOut:
            return EaseOut(initialTime, maximumTime);
        }
        return -1;
    }

    private static float EaseIn(float elapsedTime, float maxDuration)
    {
        float t = elapsedTime / maxDuration;
        return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    private static float EaseOut(float elapsedTime, float maxDuration)
    {
        float t = elapsedTime / maxDuration;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        return t;
    }
}


public class ArmMotor : MonoBehaviour
{
    private bool ready = true;
    private bool dangerous = false;
    [SerializeField] private EaseFunctionType easeFunctionSelection = EaseFunctionType.EaseOut;
    [SerializeField] public float travelDistance = 2f;
    [SerializeField] public float travelDuration = 2f;

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

        //Punch completed.
        ready = true;
    }

    public IEnumerator ArmAction(Vector3 startingPos, Vector3 newPos, float maxDuration, EaseFunctionType easeFunction)
    {
        //Begin Arm Movement
        
        float elapsedTime = 0.0f;
        while (elapsedTime <= maxDuration)
        {
            float t = EaseFunctions.Calculate(easeFunctionSelection, elapsedTime, maxDuration);

            transform.localPosition = Vector3.Lerp(startingPos, newPos, t);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //Arm movement has completed after elapsedTime > maxDuration
    }



}
