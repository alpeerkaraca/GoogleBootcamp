using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health;
    private bool dead = false;
    
    public void GetDamage(float damage)
    {
        if((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        AmIDead();
    }

    private void AmIDead()
    {
        if(health <= 0)
        {
            dead = true;
        }
    }
}
