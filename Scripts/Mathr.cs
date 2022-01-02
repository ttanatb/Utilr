using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mathr
{
    public static float Damp(float a, float b, float lambda, float dt)
    {
        return Mathf.Lerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
}
