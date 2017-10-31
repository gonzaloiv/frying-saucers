using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject enemyTypeLabelPrefab;

    public List<GameObject> CurrentWaveEnemies { get { return currentWaveEnemies; } }

    private EnemyTypeLabelSpawner enemyTypeLabelSpawner;
    private WaveSpawner waveSpawner;
    private List<GameObject> currentWaveEnemies;
    private GameObject player;

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

    void OnEnable () {
        Player.PlayerHitEvent += OnPlayerHitEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
    }

    void OnDisable () {
        Player.PlayerHitEvent -= OnPlayerHitEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init (GameObject player) {
        this.player = player;
    }

    public void NewWave (LevelType levelType, WaveData waveData) {
        currentWaveEnemies = new List<GameObject>();
        enemyTypeLabelSpawner.Init();
        switch (levelType) {
        case LevelType.RandomLevel:
            currentWaveEnemies = waveSpawner.SpawnRandomWaveEnemies(player);
            break;
        default:
            currentWaveEnemies = waveSpawner.SpawnWaveEnemies(waveData.EnemyTypes, player);
            break;
        }
        currentWaveEnemies.ForEach(enemy => enemyTypeLabelSpawner.AddGesture(enemy.GetComponent<EnemyController>().Enemy));
        enemyTypeLabelSpawner.ShowGestures(2);
    }

    public void Reset () {
        if (currentWaveEnemies != null)
            currentWaveEnemies.ForEach(enemy => {
                if (enemy != null)
                    enemy.SetActive(false);
            });
    }

    public void AddEnemy (int index) {
        GameObject enemy = waveSpawner.SpawnRandomEnemy(index, player);
        enemyTypeLabelSpawner.SetGestureByIndex(index, enemy.GetComponent<EnemyController>().Enemy);
        enemyTypeLabelSpawner.ShowGestures(1);
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        enemyTypeLabelSpawner.ShowGestures(2);
    }

    public void OnEnemyHitEvent () {
        StartCoroutine(EnemyHitRoutine());
    }

    public GameObject GetRandomActiveEnemy () {
        currentWaveEnemies = currentWaveEnemies.Where(enemy => enemy.activeInHierarchy).ToList();
        return currentWaveEnemies[Random.Range(0, currentWaveEnemies.Count)];
    }

    #endregion

    #region Private Behaviour

    private IEnumerator EnemyHitRoutine () {
        yield return new WaitForSeconds(1);
        currentWaveEnemies = currentWaveEnemies.Where(enemy => enemy.activeInHierarchy).ToList();
        if (currentWaveEnemies.Count() == 0)
            WaveEndEvent.Invoke();
    }

    #endregion


}