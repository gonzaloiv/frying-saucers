using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Fields

    [SerializeField] GameObject jetPrefab;
    [SerializeField] GameObject explosionPrefab;

    private GameObject jet;
    private ParticleSystem explosion;
    private Animator anim;
    private Player player;
    private PlayerWeaponController playerWeapon;

    private bool shot;

    #endregion

    #region Mono Behaviour

    void Awake () {
        anim = GetComponent<Animator>();
        jet = Instantiate(jetPrefab, transform);
        explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
        playerWeapon = GetComponent<PlayerWeaponController>();
    }

    void OnEnable () {
        anim.Play("Spawn");
    }

    void OnDisable () {
        jet.SetActive(false);
    }

    void OnParticleCollision (GameObject particle) {
        if (!shot && particle.layer == (int) CollisionLayer.Enemy) {
            player.DecreaseLives();
            anim.Play("Disable");
            explosion.Play();
            shot = true;
        }
    }

    #endregion

    #region Public Behaviour

    public void Init (Player player) {
        this.player = player;
    }

    public void Reset () {
        playerWeapon.enabled = true;
        jet.SetActive(true);
        shot = false;
    }

    public void Disable () {
        gameObject.SetActive(false);
    }

    #endregion

}