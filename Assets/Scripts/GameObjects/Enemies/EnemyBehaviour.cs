using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviourStates;

public class EnemyBehaviour : StateMachine, IEnemyBehaviour {

    #region Fields

    [SerializeField] private GameObject laserPrefab;

    public GameObject Player { get { return player; } }
    public ParticleSystem Laser { get { return laser; } }
    public bool Hit { get { return hit; } }
    public float RoutineTime { get { return routineTime; } }
    
    private ParticleSystem laser;
    private GameObject player;
    private bool hit = false;
    private float routineTime;

    private Coroutine enemyRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
    }

    void Update () {
        CurrentState.Play();
    }

    void OnEnable () {
        ChangeState<IdleState>();
    }

    #endregion

    #region IEnemyBehaviour

    public void Init (GameObject player) {
        this.player = player;
    }

    public void Play (float routineTime) {
        this.routineTime = routineTime;
        enemyRoutine = StartCoroutine(EnemyRoutine());
    }

    public void Stop () {
        StopAllCoroutines();
    }

    #endregion

    #region Private Behaviour

    private IEnumerator EnemyRoutine () {
        hit = false;
        ChangeState<ShootingState>();  
        yield return new WaitForSeconds(routineTime); // Depends on ShootingRoutine() in the ShootingState
        ChangeState<IdleState>();
    }

    #endregion
	
}
