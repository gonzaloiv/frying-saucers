using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviourStates;

public class EnemyBehaviour : StateMachine {

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

    #endregion

    #region Events

    public delegate void EnemyAttackEventHandler (EnemyAttackEventArgs enemyAttackEventArgs);
    public static event EnemyAttackEventHandler EnemyAttackEvent = delegate {};

    public delegate void EnemyShotEventHandler (EnemyShotEventArgs enemyShotEventArgs);
    public static event EnemyShotEventHandler EnemyShotEvent = delegate {};

    public delegate void RightGestureInputEventHandler (RightGestureInputEventArgs rightGestureInputEventArgs);
    public static event RightGestureInputEventHandler RightGestureInputEvent = delegate {};

    public delegate void WrongGestureInputEventHandler (WrongGestureInputEventArgs wrongGestureInputEventArgs);
    public static event WrongGestureInputEventHandler WrongGestureInputEvent = delegate {};

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
        StartCoroutine(EnemyRoutine());
    }

    public void Stop () {
        StopAllCoroutines();
    }

    public void InvokeEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        EnemyAttackEvent.Invoke(enemyAttackEventArgs);
    }

    public void InvokeEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        EnemyShotEvent.Invoke(enemyShotEventArgs);
    }

    public void InvokeRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        RightGestureInputEvent.Invoke(rightGestureInputEventArgs);
    }

    public void InvokeWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputEventArgs) {
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
