using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimit : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
