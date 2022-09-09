using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<PoolableMono> _poolingList;

    private void Awake() {
        if(Instance == null) Instance = this;
        // if(Instance != null) { Debug.LogWarning("Multiple GameManager Instance is Running, Destroy This"); Destroy(gameObject); return; }
        // else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }

        PoolManager.Instance = new PoolManager(transform.GetChild(0));
        foreach(PoolableMono p in _poolingList){
            PoolManager.Instance.CreatePool(p, 10);
        }

        CoinManager.Instance = gameObject.AddComponent<CoinManager>();
        SlotManager.Instance = gameObject.AddComponent<SlotManager>();
        CardManager.Instance = gameObject.GetComponent<CardManager>();
        StageManager.Instance = gameObject.GetComponent<StageManager>();
    }
}
