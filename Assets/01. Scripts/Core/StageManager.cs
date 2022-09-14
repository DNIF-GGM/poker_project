using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] private float timeLimit = 300f;
    public bool OnFight { get; private set; }= false;
    private float elapsedTime = 0;
    private int currentStageIndex = 0;
    private Theme currentTheme = null;
    private GameObject readyToFightPanel = null;
    private GameObject cardPanel = null;

    [field: SerializeField]
    public List<Stage> Stages { get; set; } = new List<Stage>();

    private void Awake()
    {
        readyToFightPanel = GameObject.Find("UICanvas/ReadyToFightPanel");
        cardPanel = GameObject.Find("CardCanvas");
    }

    private void Start()
    {
        LoadStage(currentStageIndex);
        CardManager.Instance.CardSpawn();
    }

    //스테이지 시작
    public void StartStage()
    {
        Debug.Log("시작");
        OnFight = true;

        readyToFightPanel.SetActive(false);
        cardPanel.SetActive(false);

        CardManager.Instance.ClearCard();
        CardManager.Instance.SpawnUnit();
    }

    private void Update()
    {
        if(!OnFight) return;

        elapsedTime += Time.deltaTime;
        
        if(elapsedTime >= timeLimit)
            StageOver(true);
    }

    //스테이지 종료 (유닛이 모두 죽거나 시간 초과 되거나 적이 모두 죽으면 실행)
    //유닛이 죽는 메소드에서 남은 유닛 개수 비교하여 실행 => isLose = true;
    //적이 죽는 메소드에서 남은 적 개수 비교하여 실행 => isLose = false;
    public void StageOver(bool isLose)
    {
        OnFight = false;
        
        elapsedTime = 0f;

        if(isLose) //졌을 때
        {
            CoinManager.Instance.UpdateCoin(0f);
            //패배 알림 패널 띄우기
        }
        else //이겼을 때
        {
            float coinIncreasePercent = 0.5f; //공식 만들어서 정해야됨
            CoinManager.Instance.UpdateCoin(coinIncreasePercent);
            //승리 알림 패널 띄우기
        }

        CardManager.Instance.CardSpawn();
        currentStageIndex++;
    }

    public void LoadStage(int stageIndex)
    {
        if(Stages.Count - 1 < stageIndex) return;

        if(stageIndex % 10 == 0){  //10스테이지 마다 새로운 테마 생성
            if(currentTheme != null) PoolManager.Instance.Push(currentTheme);
            Theme theme = PoolManager.Instance.Pop(Stages[stageIndex].themeBackGound.name) as Theme;
            currentTheme = theme;
        }
        
        for(int i = 0; i < Stages[stageIndex].stageMonster.Count; i++){
            //Monster monster = PoolManager.Instance.Pop(Stages[stageIndex].stageMonster[i].name) as Monster;
            //monster.transform.position = ;
            //monster.transform.rotation = ;

            Debug.Log($"{i} 번 째 몬스터");
        }

        Debug.Log($"{stageIndex} 번 스테이지 생성");
    }
}
