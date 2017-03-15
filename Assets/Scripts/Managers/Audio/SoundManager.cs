using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class SoundManager : MonoBehaviour {

  #region Fields

  // Input
  [SerializeField] private AudioClip[] rightGesture;
  [SerializeField] private AudioClip[] wrongGesture;

  // Game Mechanics
  [SerializeField] private AudioClip[] enemyAttack;
  [SerializeField] private AudioClip enemyShot;
  [SerializeField] private AudioClip enemyHit;
  [SerializeField] private AudioClip playerShot;
  [SerializeField] private AudioClip playerHit;

  // Game
  [SerializeField] private AudioClip newGame;
  [SerializeField] private AudioClip gameOver;

  private AudioSource audioSource;
  private IEnumerator enemyAttackRoutine;

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
  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    audioSource.PlayOneShot(rightGesture[Random.Range(0, rightGesture.Length)]);
    if(enemyAttackRoutine != null)
      StopCoroutine(enemyAttackRoutine);
  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
    audioSource.PlayOneShot(wrongGesture[Random.Range(0, wrongGesture.Length)]);
    if(enemyAttackRoutine != null)
      StopCoroutine(enemyAttackRoutine);
  }

  // Game Mechanics
  void OnEnemyAttackEvent(EnemyAttackEvent enemyAttackEvent) {
    enemyAttackRoutine = EnemyAttackRoutine(enemyAttackEvent.SectionTime);
    StartCoroutine(enemyAttackRoutine);
  }

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    audioSource.PlayOneShot(enemyShot);
  }

  void OnEnemyHitEvent(EnemyHitEvent EnemyHitEvent) {
    audioSource.PlayOneShot(enemyHit);
  }

  void OnPlayerShotEvent(PlayerShotEvent playerShotEvent) {
    audioSource.PlayOneShot(playerShot);
  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    audioSource.PlayOneShot(playerHit);
  }

  // Game
  void OnNewGameEvent(NewGameEvent newGameEvent) {
    audioSource.PlayOneShot(newGame);
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    audioSource.PlayOneShot(gameOver);
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EnemyAttackRoutine(float sectionTime) {
    for (int i = 0; i < Config.SHOOTING_ROUTINE_PARTS -1; i++) {
      audioSource.PlayOneShot(enemyAttack[0]);      
      yield return new WaitForSeconds(sectionTime);
    }
  }

  #endregion 

}
