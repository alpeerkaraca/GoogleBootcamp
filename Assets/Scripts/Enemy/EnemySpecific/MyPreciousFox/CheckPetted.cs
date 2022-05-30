using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.EnemySpecific.MyPreciousFox;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPetted : MonoBehaviour
{
    private FollowPlayer followPlayer { get;  set;}
    private void Start()
    {
        followPlayer = GameObject.Find("AliveFox").GetComponent<FollowPlayer>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            followPlayer.petted = true;
            gameObject.SetActive(false);
        }
    }
}
