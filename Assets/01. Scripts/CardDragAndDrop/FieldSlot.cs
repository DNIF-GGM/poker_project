using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class FieldSlot : MonoBehaviour
{
    private int cardCount = -2;

    public void SetCard()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if(SlotManager.Instance.currentSelectedCard == null) return;

        PoolableMono temp = PoolManager.Instance.Pop("Card");
        Vector3 pos = transform.position;
        pos.y += 0.52f;
        pos.x -= 0.5f * cardCount;
        temp.transform.position = pos;

        SlotManager.Instance.ResetSelectedCard();

        cardCount++;
    }
}
