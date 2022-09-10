using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multiple SceneLoader Instance is Running, Destroy This"); Destroy(gameObject); return; }
        else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }
    }

    //씬 체인지
    public void LoadScence(string sceneName)
    {
        //닷트윈 넣어야댐
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    //씬 삭제
    public void UnloadScene(string sceneName)
    {
        //닷트윈 넣어야댐
        SceneManager.UnloadSceneAsync(sceneName);
    }

    //씬 추가
    public void AddScene(string sceneName)
    {
        //닷트윈 넣어야댐
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
