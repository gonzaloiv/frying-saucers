using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreenController : MonoBehaviour {

    #region Fields

    [SerializeField] private Button tutorialButton;

    private UFOGridController ufoGridController;
    private AsyncOperation sceneLoading;

    #endregion

    #region Mono Behaviour

    void Awake () {
        ufoGridController = GetComponentInChildren<UFOGridController>();
        tutorialButton.onClick.AddListener(() => LoadScene((int) GameScene.GameScene));
    }

    void Start() {
        ufoGridController.Play();
    }

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
            ufoGridController.Stop();
            Debug.Log("Loading...");
            if (sceneLoading.progress == 0.9f)
                sceneLoading.allowSceneActivation = true;
            yield return null;
        }
    }

    #endregion

}