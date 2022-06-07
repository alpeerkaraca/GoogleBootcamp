using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CheckDialogZone : MonoBehaviour
{
    public AnyKey anyKey;

    private void Update()
    {
        if (Time.deltaTime == 2f) 
            anyKey.gameObject.SetActive(true);
    }
}
