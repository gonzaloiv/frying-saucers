using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject[] infoScreenPrefabs;
  private IInfoScreenController[] infoScreens;
  private InputManager inputManager;

  private int currentInfoScreen = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
    infoScreens = new IInfoScreenController[infoScreenPrefabs.Length];
    for (int i = 0; i < infoScreenPrefabs.Length; i++)
      infoScreens[i] = Instantiate(infoScreenPrefabs[i], transform).GetComponent<IInfoScreenController>();
  }

  void OnEnable () {

    // Input
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);

    // Game Mechanics
    EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StartListening<PlayerShotEvent>(OnPlayerShotEvent);
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);

    // Game
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);

  }

  void OnDisable () {

    // Input
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);

    // Game Mechanics
    EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StopListening<EnemyShotEvent>(OnEnemyShotEvent);
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StopListening<PlayerShotEvent>(OnPlayerShotEvent);
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);

    // Game
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);

  }

  #endregion

  #region Event Behaviour

  // Input
  void OnRightGestureInput(RightGestureInput rightGestureInput) {}

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {}

  // Game Mechanics
  void OnEnemyAttackEvent(EnemyAttackEvent enemyAttackEvent) {}

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {}

  void OnEnemyHitEvent(EnemyHitEvent EnemyHitEvent) {
    NextScreen();
  }

  void OnPlayerShotEvent(PlayerShotEvent playerShotEvent) {}

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {}

  // Game
  void OnNewGameEvent(NewGameEvent newGameEvent) {
    NextScreen();
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {}

  #endregion

  #region Private Behaviour

  private void NextScreen() {
//    infoScreens[currentInfoScreen].Play();
    infoScreens[0].Play();
    currentInfoScreen++;
  }

  #endregion

}
