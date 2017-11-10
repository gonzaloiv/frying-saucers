using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : StateMachine {

    #region Fields

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;

    public Enemy Enemy { get { return enemy; } }
    public GameObject Player { get { return player; } }
    public ParticleSystem LaserPS { get { return laserPS; } }
    public ParticleSystem ExplosionPS { get { return explosionPS; } }
    
    private Enemy enemy;
    private GameObject player;
    private ParticleSystem laserPS;
    private ParticleSystem explosionPS;

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
        laserPS = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
        explosionPS = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    }

    void OnDisable() {
        ToIdleState();
    }

    #endregion

    #region IEnemyBehaviour

    public void Init (Enemy enemy, GameObject player) {
        this.enemy = enemy;
        this.player = player;
    }

    public void ToIdleState () {
        ChangeState<IdleState>();
    }

    public void ToAttackState () {
        ChangeState<AttackState>();
    }

    public void ToDisableState () {
        ChangeState<DisableState>();
    }

    public void Disable () { // Called from "Disable" animation
        gameObject.SetActive(false);
        ChangeState<IdleState>();
        StopAllCoroutines();
    }

    public void InvokeEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        EnemyAttackEvent.Invoke(enemyAttackEventArgs);
    }

    public void InvokeEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        EnemyShotEvent.Invoke(enemyShotEventArgs);
    }

    public void InvokeEnemyHitEvent () {
        EnemyHitEvent.Invoke();
    }

    #endregion

}
