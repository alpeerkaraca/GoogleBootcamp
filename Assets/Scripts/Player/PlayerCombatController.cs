using UnityEngine;

namespace Player
{
    public class PlayerCombatController : MonoBehaviour
    {
        private CharacterController charController;
        private AttackDetails attackDetails;
        private Stats stats;
        private void Start()
        {
            charController = GetComponent<CharacterController>();
            stats = GetComponent<Stats>();
        }
        
        private void Damage(AttackDetails details)
        {
            if (!charController.GetDashStatus())    //Player won't take damage if dashing.
            {
                int direction;

                stats.DecreaseHp(details.damageAmount);
            
                if (details.position.x < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
                charController.Knockback(direction);
            }
        }
    }
    
}
