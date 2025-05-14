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
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    public void StartGame() //게임 시작 함수
    {
        uiManager.SetPlayGame();

        SetStage();
    }

    private void SetStage()
    {
        currentStageIndex += 1;

        mapCreator.CreateStage();

        StartCoroutine(DelayStartWave(3f)); // 3초 후 SetStage 실행

        uiManager.ChangeWave(currentStageIndex);
    }

    public void ClearStage()
    {
        StartCoroutine(HandleStageClear());

        mapResseter.ClearMap();
        SetStage();
    }

    public void GameOver()
    {
        monsterManager.StopWave();
        uiManager.SetGameOver();
    }

    public IEnumerator DelayStartWave(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds); // 지정된 시간만큼 대기
        monsterManager.StartWave(); // 지연 후 실행
    }

    private IEnumerator HandleStageClear()
    {
        // 1. 클리어 문구 표시
        uiManager.ShowClearText();

        // 2. 1초 대기
        yield return new WaitForSeconds(1f);

        // 3. 클리어 문구 숨기기
        uiManager.HideClearText();

    }
}
