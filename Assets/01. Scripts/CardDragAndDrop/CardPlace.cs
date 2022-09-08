using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlace : MonoBehaviour
{
    [SerializeField] CardPlace card;

    public void SelectCard()
    {
        SlotManager.Instance.currentSelectedCard = this;
    }
}
