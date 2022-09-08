using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance = null;
    public CardPlace currentSelectedCard = null;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetSelectedCard()
    {
        currentSelectedCard.gameObject.SetActive(false);
        currentSelectedCard = null;
    }
}
