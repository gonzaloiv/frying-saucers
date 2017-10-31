using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreenController : MonoBehaviour {

    #region Fields

    private AsyncOperation sceneLoading;

    #endregion

    #region Public Behaviour

    public void LoadScene (int scene) {
        StartCoroutine(LoadSceneRoutine(scene));
    }

    #endregion

    #region Private Behaviour

    public IEnumerator LoadSceneRoutine (int scene) {
        sceneLoading = SceneManager.LoadSceneAsync(scene);
        sceneLoading.allowSceneActivation = false;
        while (!sceneLoading.isDone) {
            Debug.Log("Loading...");
            if (sceneLoading.progress == 0.9f)
                sceneLoading.allowSceneActivation = true;
            yield return null;
        }
    }

    #endregion

}