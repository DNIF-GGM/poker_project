using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance = null;
    public Card currentSelectedCard = null;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            if(currentSelectedCard == null){
                RaycastHit hit = CastRay();

                if(hit.collider != null){
                    if(!hit.collider.CompareTag("Card")) return;

                    currentSelectedCard = hit.collider.GetComponent<Card>();
                    Cursor.visible = false;
                }
            }
        }

        if(currentSelectedCard != null){
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(currentSelectedCard.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            currentSelectedCard.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
        }
        else{
            RaycastHit hit = CastRay();

            if(hit.collider != null){
                if(!hit.collider.CompareTag("Card")) return;

                //hit.collider.GetComponent<RectTransform>().
            }
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
        currentSelectedCard.gameObject.SetActive(false);
        currentSelectedCard = null;
    }
}
