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
    
    private float[] attackInfos = new float[2];

    private float timerStart;
    public Rigidbody2D rb;
    private void Start()
    {
        rb.velocity = transform.right * speed;
        attackInfos[0] = damage;
        timerStart = Time.time;
    }

    private void Update()
    {
        attackInfos[1] = transform.position.x;
        if(Time.time > timerStart + timeShouldPassBeforeDestroy)
            Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.transform.parent.SendMessage("Damage",attackInfos);
        /*
        Enemy1AI enemy = col.GetComponent<Enemy1AI>();
        if (enemy != null)
        {
            enemy.Damage(attackInfos);
        }*/
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
