using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float maxHp;

    [SerializeField] private GameObject
        deathChunkPart,
        deathBloodPart;

    private GameManager gm;

    private float currentHP;

    private void Start()
    {
        currentHP = maxHp;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    public void DecreaseHP(float amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
        
    }

    
    //Other Functions
    private void Die()
    {
        Instantiate(deathChunkPart, transform.position, deathChunkPart.transform.rotation);
        Instantiate(deathBloodPart, transform.position, deathBloodPart.transform.rotation);
        gm.Respawn();
        Destroy(gameObject);
    }
    
    
}
