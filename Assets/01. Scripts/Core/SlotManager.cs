using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance = null;
    public CardUI currentSelectedCard = null;

    public void ResetSelectedCard()
    {
        currentSelectedCard.gameObject.SetActive(false);
        currentSelectedCard = null;
    }
}
