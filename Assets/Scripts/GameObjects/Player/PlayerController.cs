using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerController : StateMachine {

    #region Fields

    [SerializeField] GameObject jetPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] private GameObject laserPrefab;

    public Player Player { get { return player; } }
    public ParticleSystem ExplosionPS { get { return explosionPS; } }
    public ParticleSystem LaserPS { get { return laserPS; } } 
    public Vector2 EnemyPosition { get { return enemyPosition; } set { enemyPosition = value; } }

    private Player player;
    private ParticleSystem explosionPS;
    private ParticleSystem laserPS;
    private Vector2 enemyPosition;

    #endregion

    #region Events

    public delegate void PlayerShotEventHandler ();
    public static event PlayerShotEventHandler PlayerShotEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        Instantiate(jetPrefab, transform);
        laserPS = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
        explosionPS = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    }

    void OnEnable() {
        ToIdleState();
    }

    #endregion

    #region Public Behaviour

    public void Init (Player player) {
        this.player = player;
    }

    public void ToIdleState () {
        ChangeState<IdleState>();
    }

    public void ToEnemyAttackState () {
        ChangeState<EnemyAttackState>();
    }

    public void ToEvasionState () {
        ChangeState<EvasionState>();
    }

    public void ToRespawnState () {
        ChangeState<RespawnState>();
    }

    public void InvokePlayerShotEvent() {
        PlayerShotEvent.Invoke();
    }

    public void Disable () { // Called from "Disable" animation
        gameObject.SetActive(false);
    }

    #endregion


}