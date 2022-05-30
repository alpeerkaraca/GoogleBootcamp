using UnityEngine;

namespace Enemy.States.Data
{
 [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entitiy Data/Base Data")]
 public class EntityData : ScriptableObject
 {
  public float ledgeCheckDistance = 0.4f ;

  public LayerMask whatIsGround;
  public LayerMask whatIsPlayer;

  public float minAgroDistance = 3f;
  public float maxAgroDistance = 4f;

 }
}
