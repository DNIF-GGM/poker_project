using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    [SerializeField] private Image _loadingPanel;
    [SerializeField] private Slider _loadingSlider;

    private void Awake() {
        if(Instance == null) Instance = this;

        SetLoadingPanel();
    }
    
    private void SetLoadingPanel(){
        _loadingPanel = GameObject.Find("UICanvas/Loading").GetComponent<Image>();
        _loadingSlider = GameObject.Find("UICanvas/Loading/Slider").GetComponent<Slider>();

        _loadingPanel.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        _loadingPanel.gameObject.SetActive(true);
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOper.isDone)
        {
            yield return null;
            _loadingSlider.value = asyncOper.progress;
        }
        yield return new WaitForSeconds(0.01f);
        SetLoadingPanel();
    }
}
