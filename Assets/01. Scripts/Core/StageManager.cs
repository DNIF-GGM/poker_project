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
    [field : SerializeField]
    public List<UnitBase> Units { get; private set; } = new List<UnitBase>();
    [field : SerializeField]
    public List<MonsterBase> Monsters { get; private set; } = new List<MonsterBase>();

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

        UnitBase[] units = GameObject.Find("Pool").GetComponentsInChildren<UnitBase>();

        foreach(UnitBase u in units)
        {
            if(u.CompareTag("Enemy"))
                u.StartFight();
        }
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
        if(!OnFight) return;

        OnFight = false;
        
        elapsedTime = 0f;

        if(isLose) //졌을 때
        {
            CoinManager.Instance.UpdateCoin(0f);
            //패배 알림 패널 띄우기
        }
        else //이겼을 때
        {
            float coinIncreasePercent = 1; //공식 만들어서 정해야됨
            CoinManager.Instance.UpdateCoin(coinIncreasePercent);
            //승리 알림 패널 띄우기
        }

        CoinManager.Instance.CoinBetting(-CoinManager.Instance.BettedCoin);

        readyToFightPanel.SetActive(true);
        cardPanel.SetActive(true);

        foreach(MonsterBase m in Monsters) PoolManager.Instance.Push(m);
        foreach(UnitBase u in Units) PoolManager.Instance.Push(u);
        CardManager.Instance.CardSpawn();
        currentStageIndex++;

        LoadStage(currentStageIndex);
    }

    public void LoadStage(int stageIndex)
    {
        if(Stages.Count - 1 < stageIndex) return;

        if(stageIndex % 10 == 0){  //10스테이지 마다 새로운 테마 생성
            if(currentTheme != null) PoolManager.Instance.Push(currentTheme);
            Theme theme = PoolManager.Instance.Pop(Stages[stageIndex].themeBackGound.name) as Theme;
            currentTheme = theme;
        }

        Monsters.Clear();
        Units.Clear();
        
        for(int i = 0; i < Stages[stageIndex].stageMonster.Count; i++){
            MonsterBase monsterBase = PoolManager.Instance.Pop(Stages[stageIndex].stageMonster[0].name) as MonsterBase;
            monsterBase.transform.position = monsterBase.SpawnPos;
            Monsters.Add(monsterBase);

           Debug.Log($"{i} 번 째 몬스터");
        }

        Debug.Log($"{stageIndex} 번 스테이지 생성");
    }
}
