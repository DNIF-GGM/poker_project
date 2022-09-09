using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] private float timeLimit = 300f;
    public bool OnFight { get; private set; }= false;
    private float elapsedTime = 0;
    private int currentStageIndex = 1;
    //private Stage currentStage = null;

    //스테이지 시작
    public void StartStage()
    {
        OnFight = true;

        //전투대기 패널 끄기

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
        UnloadStage();
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
    }

    public void UnloadStage()
    {
        //PoolManager.Instance.Push(currnetStage);
    }

    public void LoadStage(int stageIndex)
    {
        PoolManager.Instance.Pop("Stage" + stageIndex);
    }
}
