using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileManager : MonoBehaviour
{
    private static MonsterProjectileManager instance;
    public static MonsterProjectileManager Instance{get{return instance;}}

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }
    
    public void ShootBullet(MonsterRangeWeapon monsterRangeWeapon, Vector2 startPostiion, Vector2 direction )
    {
        GameObject origin = projectilePrefabs[monsterRangeWeapon.BulletIndex];
        GameObject obj = Instantiate(origin,startPostiion,Quaternion.identity);
        
        MonsterProjectileController monsterProjectileController = obj.GetComponent<MonsterProjectileController>();
        monsterProjectileController.Init(direction, monsterRangeWeapon);
    }

}
