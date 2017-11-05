using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerController : StateMachine {

    #region Fields

    [SerializeField] GameObject jetPrefab;
    [SerializeField] GameObject explosionPrefab;

    public Player Player { get { return player; } }
    public ParticleSystem ExplosionPS { get { return explosionPS; } }

    private Player player;
    private ParticleSystem explosionPS;
    private Animator anim;

    #endregion

    #region Mono Behaviour

    void Awake () {
        Instantiate(jetPrefab, transform);
        explosionPS = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    void OnEnable(){
        anim.Play("Spawn");
    }

    #endregion

    #region Public Behaviour

    public void Init (Player player) {
        this.player = player;
        ToWaveState();
    }

    public void ToWaveState () {
        ChangeState<WaveState>();
    }

    public void ToWaveRestartState () {
        ChangeState<WaveRestartState>();
    }

    public void Disable () {
        gameObject.SetActive(false);
    }

    #endregion


}