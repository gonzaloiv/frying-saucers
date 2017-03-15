using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  private IGameData gameData;
  private Level[] levels;
  private int currentLevel = 0;

  private AsyncOperation sceneLoading;
  private IEnumerator loadSceneRoutine;

  #endregion

  #region Mono Behaviour

  void Awake() {

    new Board();
    levelController = GetComponentInChildren<LevelController>();
    Screen.orientation = ScreenOrientation.Portrait;

    gameData = GetComponent<IGameData>();
    gameData.InitializeLevels();
    levels = gameData.Levels;

  }

  void Start() {
    EventManager.TriggerEvent(new NewGameEvent());
  }

  void OnEnable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
    EventManager.StartListening<LevelEndEvent>(OnLevelEndEvent);
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
  }

  void OnDisable() {
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    EventManager.StopListening<LevelEndEvent>(OnLevelEndEvent);
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
  }

  #endregion

  #region Event Behaviour

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    levelController.Stop();
  }

  void OnLevelEndEvent(LevelEndEvent levelEndEvent) {
    if(currentLevel < levels.Length - 1) {
      currentLevel++;
      levelController.Play(levels[currentLevel]);
    } else {
      currentLevel = 0;
      loadSceneRoutine = LoadSceneRoutine();
      StartCoroutine(loadSceneRoutine);
    }
  }

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    gameData.InitializePlayer();
    levelController.Play(levels[currentLevel]);
  }

  #endregion

  #region Private Behaviour

  public IEnumerator LoadSceneRoutine() {

    sceneLoading = SceneManager.LoadSceneAsync(2);
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