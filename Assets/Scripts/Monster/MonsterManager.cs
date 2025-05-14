using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    private Coroutine waveRoutine;
        
    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    private List<MonsterController> activeEnemies = new List<MonsterController>(); // 현재 활성화된 적들

    private bool enemySpawnComplite;
    
    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    MiniGameManager miniManager;

    public void Init(MiniGameManager miniManager)
    {
        this.miniManager = miniManager;
    }

    public void StartWave(int waveCount)
    {
        if(waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine =  StartCoroutine(SpawnWave(waveCount));
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
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        MonsterController monsterController = spawnedEnemy.GetComponent<MonsterController>();

        GameObject player = GameObject.FindWithTag("Player");

            // ✅ Init 호출 → 플레이어를 타겟으로 설정
            monsterController.Init(this, player.transform);

        activeEnemies.Add(monsterController);
        miniManager.UIManager.ChangeCount(activeEnemies.Count); // 몬스터 카운트 추가
    }
    //적이 사망했을 때 호출되는 메서드
     public void RemoveEnemyOnDeath(MonsterController enemy)
    {
        activeEnemies.Remove(enemy);

        miniManager.UIManager.ChangeCount(activeEnemies.Count); // 몬스터 카운트 추가
        if (enemySpawnComplite && activeEnemies.Count == 0)
              miniManager.EndOfWave();
     }

    // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartWave(1);
    //    }
    //}
}
