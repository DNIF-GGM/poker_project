using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance = null;

    [SerializeField] List<PoolableMono> poolingList = new List<PoolableMono>();
    public Dictionary<string, Pool<PoolableMono>> Pools { get; private set; }= new Dictionary<string, Pool<PoolableMono>>();
    private Transform parent = null;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multple " + this.GetType() + " Instance is Running, Destroy This"); Destroy(gameObject); }
        else { Instance = this; }

        parent = transform.GetChild(0);
        foreach(PoolableMono p in poolingList)
            CreatePool(p, parent);
    }

    public void CreatePool(PoolableMono _prefab, Transform _parent)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(_prefab, _parent);
        
        if(Pools.ContainsKey(_prefab.name))
        {
            Debug.LogWarning($"{_prefab.name} | Same Name of Poolable Object Already Existed at Pools, Returning");
            return;
        }

        Pools.Add(_prefab.name, pool);
    }

    public PoolableMono Pop(string _prefabName)
    {
        PoolableMono obj = null;

        if(!Pools.ContainsKey(_prefabName))
        {
            Debug.LogWarning($"{_prefabName} | Current Name of Poolable Object Doesn't Exist at Pools, Returning Nulll");
            return null;
        }

        obj = Pools[_prefabName].Pop();
        obj.transform.SetParent(null);
        obj.Reset();
        
        return obj;
    }

    public void Push(PoolableMono _obj)
    {
        if(!Pools.ContainsKey(_obj.name))
        {
            Debug.LogWarning($"{_obj.name} | Current Name of Pool Doesn't Exist at Pools, Destroy Object");
            Destroy(_obj.gameObject);
            return;
        }

        _obj.transform.SetParent(parent);
        Pools[_obj.name].Push(_obj);
    }
}
