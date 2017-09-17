using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingScreenController : MonoBehaviour {

    #region Fields

    private const float INIT_TIME = 1f;
    [SerializeField] private Image loadingScreenTitle;
    bool dataReady = false;

    #endregion

    #region Public Behaviour

    void Awake () {
        DataManager.DataLoadedEvent += OnDataLoadedEvent;
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(DataInitRoutine());
        loadingScreenTitle.color = new Color(loadingScreenTitle.color.r, loadingScreenTitle.color.g, loadingScreenTitle.color.b, 0.0f);
    }

    void Start () {
        loadingScreenTitle.DOFade(1, INIT_TIME / 4).SetEase(Ease.InFlash);
    }

    void OnDestroy () {
        DataManager.DataLoadedEvent += OnDataLoadedEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnDataLoadedEvent () {
        dataReady = true;
    }

    #endregion

    #region Private Behaviour

    private IEnumerator DataInitRoutine () {
        DataManager.Init();
        float initialTime = Time.time;
        while (Time.time < initialTime + INIT_TIME)
            yield return null;
        if (dataReady) {
            bool hasBeenTutorialPlayed = DataManager.HasBeenTutorialPlayed;
            Debug.Log("Has been tutorial played: " + hasBeenTutorialPlayed);
            int nextSceneIndex = hasBeenTutorialPlayed == true ? (int) GameScene.MainMenuScene : (int) GameScene.TutorialScene;
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            DataManager.Init();
            SceneManager.LoadScene((int) GameScene.TutorialScene);
        }
    }

    #endregion

}
