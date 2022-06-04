using System;
using Enemy;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float
        speed = 25f,
        damage = 10f,
        timeShouldPassBeforeDestroy = 5f;
    

    public GameObject impactEffect;

    private AttackDetails attackInfos;

    private float timerStart;
    public Rigidbody2D rb;
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
       //if (Time.time > timerStart + timeShouldPassBeforeDestroy) ;
        Destroy(gameObject,timeShouldPassBeforeDestroy);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        attackInfos.damageAmount = damage;
        attackInfos.position = transform.position;
        col.gameObject.transform.parent.SendMessage("Damage",attackInfos);
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
