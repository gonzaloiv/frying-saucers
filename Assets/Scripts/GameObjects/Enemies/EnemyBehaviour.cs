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

    #region Events

    public delegate void EnemyAttackEventHandler (EnemyAttackEventArgs enemyAttackEventArgs);
    public static event EnemyAttackEventHandler EnemyAttackEvent;

    public delegate void EnemyShotEventHandler (EnemyShotEventArgs enemyShotEventArgs);
    public static event EnemyShotEventHandler EnemyShotEvent;

    public delegate void RightGestureInputEventHandler (RightGestureInputEventArgs rightGestureInputEventArgs);
    public static event RightGestureInputEventHandler RightGestureInputEvent;

    public delegate void WrongGestureInputEventHandler (WrongGestureInputEventArgs wrongGestureInputEventArgs);
    public static event WrongGestureInputEventHandler WrongGestureInputEvent;

    #endregion

    #region Mono Behaviour

    void Awake () {
        laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
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

    public void InvokeEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        if (EnemyAttackEvent != null)
            EnemyAttackEvent.Invoke(enemyAttackEventArgs);
    }

    public void InvokeEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        if (EnemyShotEvent != null)
            EnemyShotEvent.Invoke(enemyShotEventArgs);
    }

    public void InvokeRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        if (RightGestureInputEvent != null)
            RightGestureInputEvent.Invoke(rightGestureInputEventArgs);
    }

    public void InvokeWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputEventArgs) {
        if (WrongGestureInputEvent != null)
            WrongGestureInputEvent.Invoke(wrongGestureInputEventArgs);
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
