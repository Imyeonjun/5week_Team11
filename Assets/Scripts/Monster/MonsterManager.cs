using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    [SerializeField] MonsterSetter monsterSetter;

    MiniGameManager miniManager;


    public void Init(MiniGameManager miniManager)
    {
        this.miniManager = miniManager;
    }

    public void StartWave(int waveCount)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        monsterSetter.Init();

        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count); // 몬스터 카운트 추가
    }

    //적이 사망했을 때 호출되는 메서드
    public void RemoveEnemyOnDeath(MonsterController enemy)
    {
        monsterSetter.activeEnemies.Remove(enemy);

        miniManager.UIManager.ChangeCount(monsterSetter.activeEnemies.Count); // 몬스터 카운트 추가
        if (enemySpawnComplite && monsterSetter.activeEnemies.Count == 0)
            miniManager.EndOfWave();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartWave(1);
    //    }
    //}
}
