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

  private IEnumerator enemyRoutine;

  #endregion

  #region Mono Behaviour

  void Awake() {
    laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
  }

  void Update() {
    CurrentState.Play();
  }

  #endregion

  #region IEnemyBehaviour

  public void Initialize(GameObject player) {
    this.player = player;
    ChangeState<IdleState>();
  }

  public void Play() {
    enemyRoutine = EnemyRoutine();
    StartCoroutine(enemyRoutine);
  }

  public void Stop() {
    StartCoroutine(enemyRoutine);
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EnemyRoutine() {
    ChangeState<ShootingState>();  
    yield return new WaitForSeconds(2); // Depends on ShootingRoutine() in the ShootingState
    ChangeState<IdleState>();
  }

  #endregion
	
}
