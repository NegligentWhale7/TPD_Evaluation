using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float timeForLoad = 2;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Image loadingBar;

    public void LoadLevelSelected(int levelSelected)
    {
        StartCoroutine(AsyncNextScene(levelSelected));
    }

    IEnumerator AsyncNextScene(int scene)
    {
        Time.timeScale = 1;
        loadingBar.fillAmount = 0;
        loadingScreen.enabled = true;

        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(scene);
        _loadOperation.allowSceneActivation = false;
        float progressValue = 0;
        while (!_loadOperation.isDone)
        {
            progressValue = Mathf.MoveTowards(progressValue, _loadOperation.progress, Time.deltaTime * timeForLoad);
            loadingBar.fillAmount = progressValue;
            if (progressValue >= 0.9f)
            {
                loadingBar.fillAmount = 1;
                _loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
