using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance = null;
    public CardPlace currentSelectedCard = null;

    public void ResetSelectedCard()
    {
        currentSelectedCard.gameObject.SetActive(false);
        currentSelectedCard = null;
    }
}
