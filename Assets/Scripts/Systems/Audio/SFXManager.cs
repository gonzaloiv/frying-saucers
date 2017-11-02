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
        AddListeners();
    }

    void OnDestroy () {
        RemoveListeners();
    }

    void AddListeners () {

        // Input
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;

        // Game Mechanics
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent += OnEnemyShotEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
        PlayerWeaponController.PlayerShotEvent += OnPlayerShotEvent;
        Player.PlayerHitEvent += OnPlayerHitEvent;

        // Game
        GameController.NewGameEvent += OnNewGameEvent;
        LevelController.NewLevelEvent += OnNewLevelEvent;

    }

    void RemoveListeners () {

        // Input
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;

        // Game Mechanics
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent -= OnEnemyShotEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
        PlayerWeaponController.PlayerShotEvent -= OnPlayerShotEvent;
        Player.PlayerHitEvent -= OnPlayerHitEvent;

        // Game
        GameController.NewGameEvent -= OnNewGameEvent;
        LevelController.NewLevelEvent -= OnNewLevelEvent;

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

    public void OnEnemyHitEvent () {
        audioSource.PlayOneShot(enemyHit);
    }

    public void OnPlayerShotEvent () {
        audioSource.PlayOneShot(playerShot);
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        if (playerHitEventArgs.IsDead) {
            audioSource.PlayOneShot(gameOver);
        } else {
            audioSource.PlayOneShot(playerHit);
        }
    }

    // Game
    public void OnNewGameEvent () {
        audioSource.PlayOneShot(newGame);
    }

    public void OnNewLevelEvent () {
        audioSource.PlayOneShot(newLevel);
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
