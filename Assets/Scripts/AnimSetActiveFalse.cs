using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetActiveFalse : MonoBehaviour
{
    private DialogueManager sentenceCount;

    public void SetFalse()
    {
        gameObject.SetActive(false);
    }
}
