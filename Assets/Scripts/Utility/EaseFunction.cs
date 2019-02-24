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
        return Mathf.Sin(t * Mathf.PI / 2f * 0.5f);
    }

    private static float EaseExponential(float t)
    {
        float f = Mathf.Sin(t * Mathf.PI / 2f * 0.5f);
        return Mathf.Pow(f, 2);
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