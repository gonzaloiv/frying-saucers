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
    public EnemyTypeLabelSpawner EnemyTypeLabelSpawner { get { return enemyTypeLabelSpawner; } }
    public WaveSpawner WaveSpawner { get { return waveSpawner; } }
    public GameObject Player { get { return player; } }

    public GameObject[] CurrentWaveEnemies { get { return currentWaveEnemies; } }
    public WaveData CurrentWaveData { get { return currentWaveData; } }

    private EnemyTypeLabelSpawner enemyTypeLabelSpawner;
    private WaveSpawner waveSpawner;
    private GameObject player;

    private GameObject[] currentWaveEnemies;
    private WaveData currentWaveData;

    #endregion

    #region Events

    public delegate void WaveEndEventHandler ();
    public static event WaveEndEventHandler WaveEndEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemyTypeLabelSpawner = Instantiate(enemyTypeLabelPrefab, transform).GetComponent<EnemyTypeLabelSpawner>();
        waveSpawner = GetComponent<WaveSpawner>();
    }

    void OnDisable () {
        gestureManager.SetActive(false);
    }

    #endregion

    #region Public Behaviour

    public void Init (GameObject player) {
        this.player = player;
    }

    public void InitWave (LevelType levelType, WaveData waveData) {
        this.currentWaveData = waveData;
        ResetWaveEnemies();
        currentWaveEnemies = levelType == LevelType.RandomLevel ? waveSpawner.SpawnRandomWaveEnemies(waveData, player) : waveSpawner.SpawnWaveEnemies(waveData, player);
        enemyTypeLabelSpawner.Init(currentWaveEnemies);
        ToWaveStartState();
    }

    public void ToWaveStartState () {
        ChangeState<WaveStartState>();
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

    public void InvokeWaveEndEvent () {
        WaveEndEvent.Invoke();
    }

    #endregion

    #region Private Behaviour

    private void ResetWaveEnemies () {
        if (currentWaveEnemies == null)
            return;
        foreach (GameObject enemy in currentWaveEnemies)
            if (enemy != null)
                enemy.SetActive(false);
        currentWaveEnemies = null;
    }

    #endregion


}