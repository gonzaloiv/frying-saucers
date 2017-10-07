using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Fields

    [SerializeField] GameObject jetPrefab;
    private GameObject jet;

    [SerializeField] GameObject explosionPrefab;
    private ParticleSystem explosion;

    private Animator anim;
    private PlayerWeaponController playerWeapon;

    private bool shot;

    #endregion

    #region Events

    public delegate void PlayerHitEventHandler (PlayerHitEventArgs playerHitEventArgs);
    public static event PlayerHitEventHandler PlayerHitEvent;

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
            if (PlayerHitEvent != null)
                PlayerHitEvent.Invoke(new PlayerHitEventArgs());
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
