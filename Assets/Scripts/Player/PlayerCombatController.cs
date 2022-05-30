using UnityEngine;

namespace Player
{
    public class PlayerCombatController : MonoBehaviour
    {
        private CharacterController charController;
        private float[] attackDetails = new float[2];
        private Stats stats;
        private void Start()
        {
            charController = GetComponent<CharacterController>();
            stats = GetComponent<Stats>();
        }

        private void Damage(float[] attackDetails)
        {
            if (!charController.GetDashStatus())    //Player won't take damage if dashing.
            {
                int direction;

                stats.DecreaseHP(attackDetails[0]);
            
                if (attackDetails[1] < transform.position.x)
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
