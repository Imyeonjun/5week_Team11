using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] MonsterSetter monsterSetter;

    MiniGameManager miniManager;

    private bool enemySpawnComplite;

    public void Init(MiniGameManager miniManager)
    {
        this.miniManager = miniManager;
    }

    public void StartWave()
    {
        SpawnMonster();
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private void SpawnMonster()
    {
        monsterSetter.Init(); // 몬스터 일괄 생성
        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count);
        enemySpawnComplite = true;
    }

    //적이 사망했을 때 호출되는 메서드
    public void RemoveEnemyOnDeath(MonsterController enemy)
    {
        monsterSetter.activeEnemies.Remove(enemy);

        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count); // 몬스터 카운트 추가
        if (enemySpawnComplite && monsterSetter.activeEnemies.Count == 0)
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
