using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;

    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(PlayerWeaponHandler playerWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[playerWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, playerWeaponHandler, this);
    }

    //public void CreatImpactParticleAtPosition(Vector3 position, PlayerWeaponHandler weaponHandler)
    //{
    //    impactParticleSystem.transform.position = position;
    //    ParticleSystem.EmissionModule em = impactParticleSystem.emission;
    //    em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

    //    ParticleSystem.MainModule mainModule = impactParticleSystem.main;
    //    mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
    //    impactParticleSystem.Play();

    //}
}
