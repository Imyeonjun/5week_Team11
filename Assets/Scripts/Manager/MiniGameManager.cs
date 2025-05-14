using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public PlayerController playerController { get; private set; }
    private ResourceController resourceController;

    [SerializeField] private MapResseter mapResseter;
    [SerializeField] private MapCreator mapCreator;
    [SerializeField] private BossRoomCreator bossRoomCreator;
    private MonsterManager monsterManager;

    public UIManager UIManager => uiManager;
    private UIManager uiManager;

    [SerializeField] public int currentStageIndex = 0;

    public static bool isFirstLoading = true;

    private void Awake()
    {
        Instance = this;

        playerController = FindObjectOfType<PlayerController>();
        playerController.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        monsterManager = GetComponentInChildren<MonsterManager>();
        monsterManager.Init(this);

        resourceController = playerController.GetComponent<ResourceController>();
        resourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        resourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        //uiManager.ChangePlayerHP(resourceController.CurrentHealth, resourceController.MaxHealth);
    }

    private void Start()
    {
        currentStageIndex = 0;

        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    public void StartGame() //���� ���� �Լ�
    {
        uiManager.SetPlayGame();

        SetStage();
    }

    private void SetStage()
    {
        currentStageIndex += 1;

        if (currentStageIndex % 5 == 0)
        {
            bossRoomCreator.CreateBossRoom();

            StartCoroutine(DelaySpawnBoss(3f)); // 3�� �� SetStage ����

            uiManager.ChangeWave(currentStageIndex);
        }
        else
        {
            mapCreator.CreateStage();

            StartCoroutine(DelaySpawnMonster(3f)); // 3�� �� SetStage ����

            uiManager.ChangeWave(currentStageIndex);
        }
    }

    public void ClearStage()
    {
        mapResseter.ClearMap();

        SetStage();
    }

    public void GameOver()
    {
        monsterManager.StopWave();
        uiManager.SetGameOver();
    }

    public IEnumerator DelaySpawnMonster(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds); // ������ �ð���ŭ ���
        monsterManager.SpawnMonster(); // ���� �� ����
    }
    public IEnumerator DelaySpawnBoss(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds); // ������ �ð���ŭ ���
        monsterManager.SpawnBoss(); // ���� �� ����
    }
}
