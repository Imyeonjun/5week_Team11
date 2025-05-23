using Unity.Burst.CompilerServices;
using UnityEngine;

public class MonsterProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private MonsterRangeWeapon rangeWeapon;
    
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) 
        { 
            return;
        }

        currentDuration += Time.deltaTime;

        if(currentDuration > rangeWeapon.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = direction * rangeWeapon.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if(rangeWeapon.target.value == (rangeWeapon.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resource = collision.GetComponentInParent<ResourceController>();

            if(resource != null)
            {
                resource.ChangeHealth(-rangeWeapon.Power);


                if(rangeWeapon.IsOnKnockback)
                {
                    PlayerController playerController = collision.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        playerController.ApplyKnockback(transform, rangeWeapon.KnockbackPower, rangeWeapon.KnockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }


    public void Init(Vector2 direction, MonsterRangeWeapon weaponHandler)
    {
        rangeWeapon = weaponHandler;
        
        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;
        
        if (this.direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }
    
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
