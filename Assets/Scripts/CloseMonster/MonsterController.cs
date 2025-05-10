using UnityEngine;

public class MonsterController : MonsterBase
{
    private MonsterManager monsterManager;
    private Transform target;
    
    [SerializeField] private float followRange = 15f;
    
    public void Init(MonsterManager monsterManager, Transform target)
    {
        this.monsterManager = monsterManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (_weaponHandler == null || target == null)
        {
            if(!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;            
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;
            
            if (distance <= _weaponHandler.AttackRange)
            {
                int layerMaskTarget = _weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                
                movementDirection = Vector2.zero;
                return;
            }
            
            movementDirection = direction;
        }

    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    
}
