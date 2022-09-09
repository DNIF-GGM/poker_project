using UnityEngine;

public class Card : PoolableMono {
    public override void Reset()
    {
        
    }

    public void Init(Vector3 pos, Transform parent)
    {
        transform.position = pos;
        transform.SetParent(parent);
    }
}