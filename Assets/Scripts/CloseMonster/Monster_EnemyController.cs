// using UnityEngine;

// public class EnemyController : Monster_Controller
// {
//     private Monster_Manager monsterManager;
//     private Transform target;
    
//     [SerializeField] private float followRange = 15f;
    
//     public void Init(Monster_Manager monsterManager, Transform target)
//     {
//         this.monsterManager = monsterManager;
//         this.target = target;
//     }

//     protected float DistanceToTarget()
//     {
//         return Vector3.Distance(transform.position, target.position);
//     }

//     protected void HandleAction()
//     {
//         // base.HandleAction();

//         if (weaponHandler == null || target == null)
//         {
//             if(!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;            
//             return;
//         }

//         float distance = DistanceToTarget();
//         Vector2 direction = DirectionToTarget();

//         isAttacking = false;
//         if (distance <= followRange)
//         {
//             lookDirection = direction;
            
//             if (distance <= weaponHandler.AttackRange)
//             {
//                 int layerMaskTarget = weaponHandler.target;
//                 RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
//                     (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

//                 if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
//                 {
//                     isAttacking = true;
//                 }
                
//                 movementDirection = Vector2.zero;
//                 return;
//             }
            
//             movementDirection = direction;
//         }

//     }

//     protected Vector2 DirectionToTarget()
//     {
//         return (target.position - transform.position).normalized;
//     }
    
// }
