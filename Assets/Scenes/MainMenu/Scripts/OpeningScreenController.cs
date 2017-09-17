using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningScreenController : MonoBehaviour {

    #region Fields

    private Button modeOneButton;
    private Button tutorialButton;
    private Button creditsButton;

    private Animator anim;
    private AudioSource[] loungeMusic;
    private UFOGridController ufoGridController;
    private AsyncOperation sceneLoading;

    #endregion

    #region Mono Behaviour

    void Awake () {

        anim = GetComponent<Animator>();
        loungeMusic = GetComponents<AudioSource>();
        ufoGridController = GetComponentInChildren<UFOGridController>();

        modeOneButton = GetComponentsInChildren<Button>()[0];
        modeOneButton.onClick.AddListener(() => LoadScene((int) GameScene.GameScene));

        tutorialButton = GetComponentsInChildren<Button>()[1];
        tutorialButton.onClick.AddListener(() => LoadScene((int) GameScene.TutorialScene));

        creditsButton = GetComponentsInChildren<Button>()[2];
        creditsButton.onClick.AddListener(() => LoadScene((int) GameScene.CreditsScene));

    }

    void Start () {
        for (int i = 0; i < loungeMusic.Length; i++)
            loungeMusic[i].Play();
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

            anim.Play("FadeOut");
            ufoGridController.Stop();
            Debug.Log("Loading...");

            if (sceneLoading.progress == 0.9f)
                sceneLoading.allowSceneActivation = true;

            yield return null;

        }

    }

    #endregion

}