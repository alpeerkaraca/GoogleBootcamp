using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float demage;
    bool colliderBusy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;

            other.SendMessage("GetDamage", demage);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "layer")
        {
            colliderBusy = false;
        }
        
    }


}
