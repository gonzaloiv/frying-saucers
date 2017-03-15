using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningScreenController : MonoBehaviour {

  #region Fields

  [SerializeField] private int sceneToLoad;

  private Animator anim;
  private Button startSceneButton;
  private AudioSource[] loungeMusic;

  AsyncOperation sceneLoading;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
    startSceneButton = GetComponentInChildren<Button>();
    startSceneButton.onClick.AddListener(() => LoadScene());
    loungeMusic = GetComponents<AudioSource>();
  }

  void Start() {
    for(int i = 0; i < loungeMusic.Length; i++)
      loungeMusic[i].Play();
  }

  #endregion

  #region Public Behaviour

  public void LoadScene() {
    StartCoroutine(LoadSceneRoutine());
  }

  #endregion

  #region Private Behaviour

  public IEnumerator LoadSceneRoutine() {

    sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);
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