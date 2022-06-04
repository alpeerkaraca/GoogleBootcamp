using Manager;
using UnityEngine;

namespace Player
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private float maxHp;

        [SerializeField] private GameObject deathChunk;
        private bool dead;

        private GameManager gm;
        public bool tookDamage;

        private float
        currentHp;

        private HealthBar healthBar;
        
        private void Start()
        {
            currentHp = maxHp;
            dead = false;
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if(GameObject.FindWithTag("Player") != null)
                healthBar.SetMaxHP(maxHp);
        }

        private void Update()
        {   if(GameObject.FindWithTag("Player") != null)
            healthBar.SetHealth(currentHp);
        }





        public void DecreaseHp(float amount)
        {
            currentHp -= amount;

            if (currentHp <= 0)
            {
                Die();
                dead = true;
            }
            else
                dead = false;


        }
        //Other Functions
        private void Die()
        {
            gm.Respawn();
            Instantiate(deathChunk);
            Destroy(gameObject);
        }


        public bool GetDeadStatus()
        {
            return dead;
        }
    
    }
}
