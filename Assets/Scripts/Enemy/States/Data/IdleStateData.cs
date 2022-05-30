using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle Data")]
public class IdleStateData :ScriptableObject
{
 public float minIdleTime = 1f;
 public float maxIdleTime = 3f;

}
