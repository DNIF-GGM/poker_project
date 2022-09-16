using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    [SerializeField] private GameObject _loadingObj;
    [SerializeField] private Vector3 _initPos;
    [SerializeField] private Vector3 _lastPos;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multiple SceneLoader Instance is Running, Destroy This"); Destroy(gameObject); return; }
        else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }

        _loadingObj.transform.position = _initPos;
        _loadingObj.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        float currentPos = _initPos.y;
        _loadingObj.gameObject.SetActive(true);
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOper.isDone)
        {
            yield return null;
            currentPos = Mathf.Lerp(_initPos.y, _lastPos.y, asyncOper.progress);
            _loadingObj.transform.position = new Vector3(_initPos.x, currentPos, _initPos.z);
        }
    }
}
