using System;
using System.Collections;
using UnityEngine;

public class PlayerFireballAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBallPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            KatonGoukakyuuNoJutsu();
        }
        var impactDestroy = GameObject.Find("impactEffect(Clone)");         
        var hitParticleDestroy = GameObject.Find("HitParticles_0 1(Clone)");
        
        Destroy(impactDestroy,2f);
        Destroy(hitParticleDestroy,.2f);
    }

    private void KatonGoukakyuuNoJutsu()        //Note for any one who reads: Defined this function's name like this cuz felling kinda bored :/
    {
        Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }
    
}
