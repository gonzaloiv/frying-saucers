using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveController : MonoBehaviour {

    #region Fields

    public EnemySpawner EnemySpawner { get { return enemySpawner; } }
    public List<GameObject> CurrentWaveEnemyObjects { get { return currentWaveEnemyObjects; } }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyTypeLabelPrefab;

    private EnemySpawner enemySpawner;
    private EnemyTypeLabelSpawner enemyTypeLabelSpawner;
    private GameObject player;
    private List<GameObject> currentWaveEnemyObjects;

    private Enemy[] currentWaveEnemies;
    private Vector2[] currentWaveEnemyGrid;
    private IEnumerator newWaveRoutine;

    #endregion

    #region Events

    public delegate void WaveEndEventHandler ();
    public static event WaveEndEventHandler WaveEndEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemySpawner = Instantiate(enemyPrefab, transform).GetComponent<EnemySpawner>();
        enemyTypeLabelSpawner = Instantiate(enemyTypeLabelPrefab, transform).GetComponent<EnemyTypeLabelSpawner>();
    }

    void OnEnable () {
        PlayerController.PlayerHitEvent += OnPlayerHitEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
    }

    void OnDisable () {
        PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init (GameObject player) {
        this.player = player;
    }

    public void NewWave (WaveData waveData) {
        currentWaveEnemyObjects = new List<GameObject>();
        if (waveData.EnemiesType == null || waveData.EnemiesType.Length == 0) {
            // Random wave spawning
            currentWaveEnemyGrid = Board.EnemyGrid(GameConfig.RandomWaveEnemyAmount);
            currentWaveEnemies = GetRandomWaveEnemies(GameConfig.RandomWaveEnemyAmount);
            enemyTypeLabelSpawner.Init();
            for (int i = 0; i < currentWaveEnemies.Length; i++) {
                currentWaveEnemyObjects.Add(enemySpawner.SpawnEnemy(currentWaveEnemies[i], player));
                enemyTypeLabelSpawner.AddGesture(currentWaveEnemies[i]);
            }
        } else {
            // Resources-defined wave spawning
            currentWaveEnemyGrid = Board.EnemyGrid(waveData.EnemiesType.Length);
            currentWaveEnemies = new Enemy[waveData.EnemiesType.Length];
            enemyTypeLabelSpawner.Init();
            for (int i = 0; i < waveData.EnemiesType.Length; i++) {
                if (waveData.EnemiesType[i] != EnemyType.None) {
                    Enemy enemy = GetEnemyByType(waveData.EnemiesType[i], i);
                    currentWaveEnemies[i] = enemy;
                    currentWaveEnemyObjects.Add(enemySpawner.SpawnEnemy(enemy, player));
                    enemyTypeLabelSpawner.AddGesture(enemy);
                }
            }
        }
        enemyTypeLabelSpawner.ShowGestures(2);
    }

    public void Reset () {
        if (currentWaveEnemyObjects != null) {
            for (int i = 0; i < currentWaveEnemyObjects.Count; i++)
                if (currentWaveEnemyObjects[i] != null)
                    currentWaveEnemyObjects[i].SetActive(false);
        }
    }

    public void AddEnemy (int index) {
        Enemy enemy = currentWaveEnemies[index];
        enemy.SetRandomType();
        currentWaveEnemyObjects[index] = enemySpawner.SpawnEnemy(enemy, player);
        enemyTypeLabelSpawner.SetGestureByIndex(index, enemy);
        enemyTypeLabelSpawner.ShowGestures(1);
    }

    public void OnPlayerHitEvent () {
        enemyTypeLabelSpawner.ShowGestures(2);
    }

    public void OnEnemyHitEvent () {
        newWaveRoutine = NewWaveRoutine();
        StartCoroutine(newWaveRoutine);
    }

    #endregion

    #region Private Behaviour

    private Enemy[] GetRandomWaveEnemies (int enemyAmount) {
        Enemy[] enemies = new Enemy[enemyAmount];
        for (int i = 0; i < enemies.Length; i++) { 
            EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
            enemies[i] = GetEnemyByType(enemyType, i);
        }
        return enemies;
    }

    private Enemy GetEnemyByType (EnemyType enemyType, int enemyIndex) {
        EnemyScore enemyScore = (EnemyScore) (int) enemyType;
        Vector2 enemyPosition = currentWaveEnemyGrid[enemyIndex];
        return new Enemy(enemyType, enemyPosition, enemyScore);
    }

    private IEnumerator NewWaveRoutine () {
        yield return new WaitForSeconds(1);
        if (currentWaveEnemyObjects.ToList().Where(x => x.activeInHierarchy).Count() == 0) {
            WaveEndEvent.Invoke();
        }
    }

    #endregion


}