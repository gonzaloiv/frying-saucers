using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyBehaviourStates;

public class EnemyController : StateMachine {

    #region Fields

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject haloPrefab;
    [SerializeField] private GameObject explosionPrefab;

    public Enemy Enemy { get { return enemy; } }

    public GameObject Player { get { return player; } }
    public ParticleSystem Laser { get { return laser; } }
    public bool Hit { get { return hit; } }
    public float RoutineTime { get { return routineTime; } }
    
    private Enemy enemy;
    private GameObject player;

    private ParticleSystem laser;
    private ParticleSystem explosion;
    private ParticleSystem halo;
    private Animator anim;

    private bool hit = false;
    private float routineTime;

    #endregion

    #region Events

    public delegate void EnemyAttackEventHandler (EnemyAttackEventArgs enemyAttackEventArgs);
    public static event EnemyAttackEventHandler EnemyAttackEvent = delegate {};

    public delegate void EnemyShotEventHandler (EnemyShotEventArgs enemyShotEventArgs);
    public static event EnemyShotEventHandler EnemyShotEvent = delegate {};

    public delegate void EnemyHitEventHandler ();
    public static event EnemyHitEventHandler EnemyHitEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
        explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
        halo = Instantiate(haloPrefab, transform).GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    #endregion

    #region IEnemyBehaviour

    public void Init (GameObject player, Enemy enemy) {
        this.player = player;
        this.enemy = enemy;
        ChangeState<IdleState>();
    }

    public void Play (float routineTime) {
        this.routineTime = routineTime;
        StartCoroutine(EnemyRoutine());
    }

    public void Stop () {
        StopAllCoroutines();
    }

    public void DisableRoutine () {
        StopAllCoroutines();
        anim.Play("Disable");
        explosion.transform.position = transform.position;
        explosion.Play();
        EnemyHitEvent.Invoke();
    }

    public void Disable () {
        gameObject.SetActive(false);
    }

    public void PlayHalo () {
        halo.Play();
    }

    public void StopHalo () {
        halo.Stop();
    }

    public void InvokeEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        EnemyAttackEvent.Invoke(enemyAttackEventArgs);
    }

    public void InvokeEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        EnemyShotEvent.Invoke(enemyShotEventArgs);
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
