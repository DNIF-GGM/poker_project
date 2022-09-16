using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance = null;
    public Card currentSelectedCard = null;

    public bool isDrag {get; set;} = false;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            if(currentSelectedCard == null){
                RaycastHit hit = CastRay();

                if(hit.collider != null){
                    if(!hit.collider.CompareTag("Card") || isDrag) return;

                    currentSelectedCard = hit.collider.GetComponent<Card>();
                    isDrag = true;
                    Cursor.visible = false;

                    if(currentSelectedCard.Slot != null)
                    {
                        currentSelectedCard.Slot._CardSlot.Remove(currentSelectedCard.cardEnum);
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(1) && currentSelectedCard != null)
        {
            Cursor.visible = true;
            isDrag = false;

            RectTransform rect;

            Card card = PoolManager.Instance.Pop("Card") as Card;
            rect = card.GetComponent<RectTransform>();
            rect.SetParent(CardManager.Instance._cardParentTrm);
            rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, rect.anchoredPosition.y, 0);
            rect.localRotation = Quaternion.Euler(0, 0, 180);
            rect.localScale = new Vector3(100, 100, 100);

            card.CardStatusSet(currentSelectedCard.cardSO);

            ResetSelectedCard();
        }

        if(currentSelectedCard != null){
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(currentSelectedCard.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            currentSelectedCard.transform.position = new Vector3(worldPosition.x, 2, worldPosition.z);
        }
    }

    private RaycastHit CastRay(){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

    public void ResetSelectedCard()
    {
        PoolManager.Instance.Push(currentSelectedCard);
        currentSelectedCard = null;
    }
}
