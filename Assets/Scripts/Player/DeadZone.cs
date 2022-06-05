using UnityEngine;

namespace Player
{
    public class DeadZone : MonoBehaviour
    {
        private Stats stats;

        private void Update()
        {
            if (GameObject.FindWithTag("Player") != null)
                stats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            stats.DecreaseHp(Mathf.Infinity);
        }
    }
}
