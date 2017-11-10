using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using WaveStates;

public class WaveController : StateMachine {

    #region Fields

    [SerializeField] private GameObject gestureManager;
    [SerializeField] private GameObject enemyTypeLabelPrefab;

    public GameObject GestureManager { get { return gestureManager; } }
    public GameObject Player { get { return player; } }
    public EnemyGestureSpawner EnemyGestureSpawner { get { return enemyGestureSpawner; } }
    public WaveSpawner WaveSpawner { get { return waveSpawner; } }
    public Wave CurrentWave { get { return currentWave; } }

    private GameObject player;
    private EnemyGestureSpawner enemyGestureSpawner;
    private WaveSpawner waveSpawner;
    private Wave currentWave;

    #endregion

    #region Events

    public delegate void WaveEndEventHandler ();
    public static event WaveEndEventHandler WaveEndEvent = delegate {};

    public delegate void EnemyAttackStartEventHandler (float time);
    public static event EnemyAttackStartEventHandler EnemyAttackStartEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemyGestureSpawner = Instantiate(enemyTypeLabelPrefab, transform).GetComponent<EnemyGestureSpawner>();
    }

    void OnDisable () {
        gestureManager.SetActive(false);
        currentWave.ResetWaveEnemies();
    }

    #endregion

    #region Public Behaviour

    public void Init (GameObject player) {
        this.player = player;
        waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.Init(player);
        currentWave = new Wave();
    }

    public void InitWave (LevelType levelType, WaveData waveData) {
        currentWave.Init(waveData);
        ToRoundStartState();
        gestureManager.SetActive(true);
    }

    public void ToRoundStartState () {
        if (currentWave.RemainingRounds <= 0) {
            InvokeWaveEndEvent();
        } else {
            ChangeState<RoundStartState>();
        }
    }

    public void ToWaveRefillState () {
        ChangeState<WaveRefillState>();
    }

    public void ToPlayerRespawnState () {
        ChangeState<PlayerRespawnState>();
    }

    public void ToEnemyAttackState () {
        ChangeState<EnemyAttackState>();
    }

    public void ToBaseState () {
        ChangeState<BaseState>();
    }

    public void InvokeWaveEndEvent () {
        WaveEndEvent.Invoke();
    }

    public void InvokeEnemyAttackStartEvent (float time) {
        EnemyAttackStartEvent.Invoke(time);
    }

    #endregion

}