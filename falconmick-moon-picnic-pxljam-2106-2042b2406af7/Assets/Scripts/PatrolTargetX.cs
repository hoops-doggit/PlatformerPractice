using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Patrol Target", menuName = "Patrol Target X", order = 2)]
public class PatrolTargetX : ScriptableObject
{
    public float LeftTarget = 0;
    public float RightTarget = 10;
}
