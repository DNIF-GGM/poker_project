using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Slider _loadingSlider;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    // private void SetLoadingPanel(Scene scene, LoadSceneMode mode){
    //     _loadingPanel = GameObject.Find("UICanvas/Loading").GetComponent<Image>();
    //     _loadingSlider = GameObject.Find("UICanvas/Loading/Slider").GetComponent<Slider>();

    //     _loadingPanel.gameObject.SetActive(false);
    // }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        GameObject loadingPanel = GameObject.Instantiate(_loadingPanel);
        loadingPanel.transform.SetParent(GameObject.Find("UICanvas").transform);
        loadingPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        _loadingSlider = loadingPanel.GetComponentInChildren<Slider>();
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOper.isDone)
        {
            yield return null;
            _loadingSlider.value = asyncOper.progress;
        }
    }
}
