using UnityEngine;

public static class RandomizerManager
{
    public static bool RandomActivation(float probability)
    {
        return probability >= Random.Range(0f, 1f);
    }
}