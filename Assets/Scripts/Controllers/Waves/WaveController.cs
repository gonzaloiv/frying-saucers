using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyTypeLabelPrefab;

    public EnemySpawner EnemySpawner { get { return enemySpawner; } }
    public List<GameObject> CurrentWaveEnemyObjects { get { return currentWaveEnemyObjects; } }

    private EnemySpawner enemySpawner;
    private List<GameObject> currentWaveEnemyObjects;
    private EnemyTypeLabelSpawner enemyTypeLabelSpawner;

    private Enemy[] currentWaveEnemies;
    private Vector2[] currentWaveEnemyGrid;
    private IEnumerator newWaveRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemySpawner = Instantiate(enemyPrefab, transform).GetComponent<EnemySpawner>();
        enemyTypeLabelSpawner = Instantiate(enemyTypeLabelPrefab, transform).GetComponent<EnemyTypeLabelSpawner>();
    }

    void OnEnable () {
        EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
        EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
    }

    void OnDisable () {
        EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
        EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    }

    #endregion

    #region Event Behaviour

    void OnPlayerHitEvent (PlayerHitEvent playerHitEvent) {
        enemyTypeLabelSpawner.ShowGestures(2);
    }

    void OnEnemyHitEvent (EnemyHitEvent enemyHitEvent) {
        newWaveRoutine = NewWaveRoutine();
        StartCoroutine(newWaveRoutine);
    }

    void OnGameOverEvent (GameOverEvent gameOverEvent) {
        enemyTypeLabelSpawner.HideGestures();
    }

    #endregion

    #region Public Behaviour

    public void NewWave (WaveData waveData) {
        currentWaveEnemyObjects = new List<GameObject>();
        if (waveData.EnemiesType == null || waveData.EnemiesType.Length == 0) {
            // Random waves
            currentWaveEnemyGrid = Board.EnemyGrid(Config.RANDOM_WAVE_ENEMY_AMOUNT);
            currentWaveEnemies = GetRandomWaveEnemies(Config.RANDOM_WAVE_ENEMY_AMOUNT);
            enemyTypeLabelSpawner.Init();
            for (int i = 0; i < currentWaveEnemies.Length; i++) {
                currentWaveEnemyObjects.Add(enemySpawner.SpawnEnemy(currentWaveEnemies[i], player));
                enemyTypeLabelSpawner.AddGesture(currentWaveEnemies[i]);
            }
        } else {
            // Static data defined waves
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

    #endregion

    #region Private Behaviour

    private Enemy[] GetRandomWaveEnemies (int enemyAmount) {
        Enemy[] enemies = new Enemy[enemyAmount];
        for (int i = 0; i < enemies.Length; i++) { 
            EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(1, EnemyType.GetNames(typeof(EnemyType)).Length);
            enemies[i] = GetEnemyByType(enemyType, i);
        }
        return enemies;
    }

    private Enemy GetEnemyByType (EnemyType enemyType, int enemyIndex) {
        Debug.Log("EnemyType " + enemyType);
        EnemyScore enemyScore = (EnemyScore) (int) enemyType;
        Vector2 enemyPosition = currentWaveEnemyGrid[enemyIndex];
        return new Enemy(enemyType, enemyPosition, enemyScore);
    }

    private IEnumerator NewWaveRoutine () {
        yield return new WaitForSeconds(1);
        if (currentWaveEnemyObjects.ToList().Where(x => x.activeInHierarchy).Count() == 0)
            EventManager.TriggerEvent(new WaveEndEvent());
    }

    #endregion


}