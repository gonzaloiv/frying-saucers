using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    #region Mono Behaviour

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameData gameData;

    private LevelController levelController;
    private int currentLevel = 0;

    private AsyncOperation sceneLoading;
    private IEnumerator loadSceneRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        new Board();
        levelController = GetComponentInChildren<LevelController>();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start () {
        EventManager.TriggerEvent(new NewGameEvent());
        new Player(gameData.PlayerInitialLives);
        levelController.Play(gameData.Levels[currentLevel]);
    }

    void OnEnable () {
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StartListening<LevelEndEvent>(OnLevelEndEvent);
    }

    void OnDisable () {
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StopListening<LevelEndEvent>(OnLevelEndEvent);
    }

    #endregion

    #region Public Behaviour

    void OnGameOverEvent (GameOverEvent gameOverEvent) {
        levelController.Stop();
    }

    void OnLevelEndEvent (LevelEndEvent levelEndEvent) {
        if (currentLevel < gameData.Levels.Count - 1) {
            currentLevel++;
            levelController.Play(gameData.Levels[currentLevel]);
        } else {
            currentLevel = 0;
            loadSceneRoutine = LoadSceneRoutine();
            StartCoroutine(loadSceneRoutine);
        }
    }

    #endregion

    #region Private Behaviour

    public IEnumerator LoadSceneRoutine () {

        yield return new WaitForSeconds(1);

        sceneLoading = SceneManager.LoadSceneAsync((int) GameScene.MainMenuScene); // TODO: corregir esto, con un SerializedField?
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