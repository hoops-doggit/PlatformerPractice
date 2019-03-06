using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public bool UseParent = true;

    public void HitEnemy()
    {
        if (this.UseParent)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
