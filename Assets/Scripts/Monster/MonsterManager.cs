using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] MonsterSetter monsterSetter;
    [SerializeField] BossSetter bossSetter;

    MiniGameManager miniManager;

    private bool enemySpawnComplite;

    public void Init(MiniGameManager miniManager)
    {
        this.miniManager = miniManager;
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    public void SpawnMonster()
    {
        monsterSetter.Init(); // 몬스터 일괄 생성
        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count);
    }

    public void SpawnBoss()
    {
        // 보스를 특정 위치에 생성
        bossSetter.SpawnBoss(new Vector2(0f, 3f));

    }

    //적이 사망했을 때 호출되는 메서드
    public void RemoveEnemyOnDeath(MonsterController enemy)
    {
        monsterSetter.activeEnemies.Remove(enemy);

        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count); // 몬스터 카운트 추가

        if (monsterSetter.activeEnemies.Count == 0)
            miniManager.ClearStage();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartWave(1);
    //    }
    //}
}
