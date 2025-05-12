using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private PlayerWeaponHandler playerWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    ProjectileManager projectileManager;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        if (currentDuration > playerWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position);
        }

        _rigidbody.velocity = direction * playerWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f);
        }
        else if (playerWeaponHandler.target.value == (playerWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            //ResourceController resourceController = collision.GetComponent<ResourceController>();
            //if (resourceController != null)
            //{
            //    resourceController.ChangeHealth(-playerWeaponHandler.Power);
            //    if (playerWeaponHandler.IsOnKnockback)
            //    {
            //        PlayerController controller = collision.GetComponent<PlayerController>();
            //        if (controller != null)
            //        {
            //            controller.ApplyKnockback(transform, playerWeaponHandler.KnockbackPower, playerWeaponHandler.KnockbackTime);
            //        }
            //    }
            //}
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f);
        }
    }

    public void Init(Vector2 direction, PlayerWeaponHandler weaponHander, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        playerWeaponHandler = weaponHander;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHander.BulletSize;
        spriteRenderer.color = weaponHander.ProjectileColor;

        transform.right = this.direction;

        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyProjectile(Vector3 position)
    {
        Destroy(this.gameObject);
    }

}
