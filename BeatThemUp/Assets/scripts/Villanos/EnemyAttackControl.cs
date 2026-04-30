using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    //colisionador "hit box"
    [SerializeField] private Collider enemyAttackZone;

    public void EnableAttackZone()
    {
        enemyAttackZone.enabled = true;
    }

    public void DisableAttackZone()
    {
        enemyAttackZone.enabled = false;
    }
    
    void Start()
    {
        DisableAttackZone();
    }

}
