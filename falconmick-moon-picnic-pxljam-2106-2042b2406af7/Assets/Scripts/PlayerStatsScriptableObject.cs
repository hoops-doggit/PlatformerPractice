using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player Stats", order = 1)]
public class PlayerStatsScriptableObject : ScriptableObject 
{
	public int HitPoints = 40;
	public float ImmuneTime = 3f;
}
