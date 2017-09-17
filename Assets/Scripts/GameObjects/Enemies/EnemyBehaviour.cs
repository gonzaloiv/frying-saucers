using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviourStates;

public class EnemyBehaviour : StateMachine, IEnemyBehaviour {

  #region Fields

  [SerializeField] private GameObject laserPrefab;
  public ParticleSystem Laser { get { return laser; } }
  private ParticleSystem laser;

  public GameObject Player { get { return player; } }
  private GameObject player;

  public bool Hit { get { return hit; } }
  private bool hit = false;

  public float RoutineTime { get { return routineTime; } }
  private float routineTime;

  private Coroutine enemyRoutine;

  #endregion

  #region Mono Behaviour

  void Awake() {
    laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
  }

  void Update() {
    CurrentState.Play();
  }

  void OnEnable() {
    ChangeState<IdleState>();
  }

  #endregion

  #region IEnemyBehaviour

  public void Initialize(GameObject player) {
    this.player = player;
  }

  public void Play(float routineTime) {
    this.routineTime = routineTime;
    enemyRoutine = StartCoroutine(EnemyRoutine());
  }

  public void Stop() {
    StopAllCoroutines();
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EnemyRoutine() {
    hit = false;
    ChangeState<ShootingState>();  
    yield return new WaitForSeconds(routineTime); // Depends on ShootingRoutine() in the ShootingState
    ChangeState<IdleState>();
  }

  #endregion
	
}
