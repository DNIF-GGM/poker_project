using UnityEngine;

public class Pop : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float duration;

    public void DoPopUp()
    {
        //닷트윈으로 수정
        rect.gameObject.SetActive(true);
    }

    public void DoPopDown()
    {
        //닷트윈으로 수정
        rect.gameObject.SetActive(false);
    }
}
