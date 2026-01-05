using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EScenes 
{ 
    MenuScene,
    MainScene,
}

public class ScenesController : MonoBehaviour
{
    public static ScenesController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void LoadScene(EScenes scene)
    {
        StartCoroutine(LoadSceneIE(scene));
    }
    public void LoadScene(EScenes scene, Action onComplete)
    {
        StartCoroutine(LoadSceneIE(scene, onComplete));
    }

    IEnumerator LoadSceneIE(EScenes scene, Action onComplete = null)
    {
        UIManager.Instance.StartCover();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        asyncOperation.allowSceneActivation = false;

        while(asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        yield return new WaitForSeconds(1f);
        UIManager.Instance.EndCover();
        onComplete?.Invoke();
    }
}
