using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<PoolableMono> _poolingList;

    private void Awake() {
        if(Instance == null) Instance = this;

        PoolManager.Instance = new PoolManager(transform.GetChild(0));
        foreach(PoolableMono p in _poolingList){
            PoolManager.Instance.CreatePool(p, 10);
        }

        CardManager.Instance = gameObject.GetComponent<CardManager>();
        SlotManager.Instance = gameObject.AddComponent<SlotManager>();
        StageManager.Instance = gameObject.GetComponent<StageManager>();
    }
}
