using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class VoiceManager : MonoBehaviour {

  #region Fields

  // Input
  [SerializeField] private AudioClip rightGesture;
  [SerializeField] private AudioClip wrongGesture;

  // Game Mechanics
  [SerializeField] private AudioClip enemyShot;
  [SerializeField] private AudioClip enemyHit;
  [SerializeField] private AudioClip playerShot;
  [SerializeField] private AudioClip playerHit;

  // Game
  [SerializeField] private AudioClip newGame;
  [SerializeField] private AudioClip[] gameOver;

  private AudioSource audioSource;

  #endregion

  #region Mono Behaviour

  void Awake() {
    audioSource = GetComponent<AudioSource>();
  }

  void OnEnable () {

    // Input
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);

    // Game Mechanics
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

  void OnEnemyHitEvent(EnemyHitEvent EnemyHitEvent) {}

  void OnPlayerShotEvent(PlayerShotEvent playerShotEvent) {}

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    if(Player.Lives > 1)
    audioSource.PlayOneShot(playerHit);
  }

  // Game
  void OnNewGameEvent(NewGameEvent newGameEvent) {}

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    audioSource.PlayOneShot(gameOver[Random.Range(0, gameOver.Length)]);
  }

  #endregion

}
