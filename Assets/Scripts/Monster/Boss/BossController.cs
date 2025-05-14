using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    [Header("Boss Weapon")]
    [SerializeField] private MonsterClose meleeWeapon;
    [SerializeField] private MonsterRangeWeapon rangeWeapon;

    [SerializeField] private float meleeAttackRange = 3f;
    [SerializeField] private float rangeAttackRange = 10f;

    private bool isweaponReady = false;


    protected override void Start()
    {
        base.Start();
        meleeWeapon.Init(this);
        rangeWeapon.Init(this);
        isweaponReady = true;
    }

    protected override void HandleAction()
    {
        if(!isweaponReady)
            return;

            
        if (target == null)
        {
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();
        lookDirection = direction;


        if (distance <= meleeAttackRange)
        {
            movementDirection = Vector2.zero;
            meleeWeapon.Attack();
        }
        else if (distance <= rangeAttackRange)
        {
            movementDirection = Vector2.zero;
            rangeWeapon.Attack();
        }
        else
        {
            movementDirection = direction;
        }
    }
}
