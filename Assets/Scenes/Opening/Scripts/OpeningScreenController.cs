using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningScreenController : MonoBehaviour {

  #region Fields

  private Animator anim;
  private Button startSceneButton;
  AsyncOperation sceneLoading;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
    startSceneButton = GetComponentInChildren<Button>();
    startSceneButton.onClick.AddListener(() => LoadMainScene());
  }

  #endregion

  #region Public Behaviour

  public void LoadMainScene() {
    StartCoroutine(LoadMainSceneRoutine());
  }

  #endregion

  #region Private Behaviour

  public IEnumerator LoadMainSceneRoutine() {

    sceneLoading = SceneManager.LoadSceneAsync(2);
    sceneLoading.allowSceneActivation = false;

    while (!sceneLoading.isDone) {

      anim.Play("FadeOut");
      Debug.Log("Loading...");

      if (sceneLoading.progress == 0.9f)
        sceneLoading.allowSceneActivation = true;

      yield return null;

    }
  }

  #endregion

}