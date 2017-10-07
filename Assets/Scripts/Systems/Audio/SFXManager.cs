using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class SFXManager : MonoBehaviour {

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
    [SerializeField] private AudioClip newLevel;
    [SerializeField] private AudioClip gameOver;

    private AudioSource audioSource;
    private IEnumerator enemyAttackRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable () {

        // Input
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;

        // Game Mechanics
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent += OnEnemyShotEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
        PlayerWeaponController.PlayerShotEvent += OnPlayerShotEvent;
        PlayerController.PlayerHitEvent += OnPlayerHitEvent;

        // Game
        GameController.NewGameEvent += OnNewGameEvent;
        LevelController.NewLevelEvent += OnNewLevelEvent;
        LevelScreenController.GameOverEvent += OnGameOverEvent;

    }

    void OnDisable () {

        // Input
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;

        // Game Mechanics
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent -= OnEnemyShotEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
        PlayerWeaponController.PlayerShotEvent -= OnPlayerShotEvent;
        PlayerController.PlayerHitEvent -= OnPlayerHitEvent;

        // Game
        GameController.NewGameEvent -= OnNewGameEvent;
        LevelController.NewLevelEvent -= OnNewLevelEvent;
        LevelScreenController.GameOverEvent -= OnGameOverEvent;

    }

    #endregion

    #region Public Behaviour

    // Input
    public void OnRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        audioSource.PlayOneShot(rightGesture[Random.Range(0, rightGesture.Length)]);
        if (enemyAttackRoutine != null)
            StopCoroutine(enemyAttackRoutine);
    }

    public void OnWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputEventArgs) {
        audioSource.PlayOneShot(wrongGesture[Random.Range(0, wrongGesture.Length)]);
        if (enemyAttackRoutine != null)
            StopCoroutine(enemyAttackRoutine);
    }

    // Game Mechanics
    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        enemyAttackRoutine = EnemyAttackRoutine(enemyAttackEventArgs.SectionTime);
        StartCoroutine(enemyAttackRoutine);
    }

    public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEvent) {
        audioSource.PlayOneShot(enemyShot);
    }

    public void OnEnemyHitEvent (EnemyHitEventArgs enemyHitEventArgs) {
        audioSource.PlayOneShot(enemyHit);
    }

    public void OnPlayerShotEvent (PlayerShotEventArgs playerShotEventArgs) {
        audioSource.PlayOneShot(playerShot);
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        audioSource.PlayOneShot(playerHit);
    }

    // Game
    public void OnNewGameEvent (NewGameEventArgs newGameEventArgs) {
        audioSource.PlayOneShot(newGame);
    }

    public void OnNewLevelEvent (NewLevelEventArgs newLevelEventArgs) {
        audioSource.PlayOneShot(newLevel);
    }

    public void OnGameOverEvent (GameOverEventArgs gameOverEventArgs) {
        audioSource.PlayOneShot(gameOver);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator EnemyAttackRoutine (float sectionTime) {
        for (int i = 0; i < GameConfig.ShootingRoutineSections - 1; i++) {
            audioSource.PlayOneShot(enemyAttack[0]);      
            yield return new WaitForSeconds(sectionTime);
        }
    }

    #endregion

}
