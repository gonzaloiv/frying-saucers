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
    private PlayerWeaponController playerWeapon;

    private bool shot;

    #endregion

    #region Events

    public delegate void PlayerHitEventHandler ();
    public static event PlayerHitEventHandler PlayerHitEvent = delegate {};

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
            PlayerHitEvent.Invoke();
            shot = true;
            anim.Play("Disable");
            explosion.Play();
        }
    }

    #endregion

    #region Public Behaviour

    public void Initialize () {
        playerWeapon.enabled = true;
        jet.SetActive(true);
        shot = false;
    }

    public void Disable () {
        gameObject.SetActive(false);
    }

    #endregion

}
