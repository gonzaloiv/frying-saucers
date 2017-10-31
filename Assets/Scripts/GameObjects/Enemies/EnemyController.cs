using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController {

    #region Fields

    public Enemy Enemy { get { return enemy; } }

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject haloPrefab;

    private ParticleSystem explosion;
    private ParticleSystem halo;
    private Animator anim;
    private Enemy enemy;

    #endregion

    #region Events

    public delegate void EnemyHitEventHandler ();
    public static event EnemyHitEventHandler EnemyHitEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
        halo = Instantiate(haloPrefab, transform).GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    void OnEnable () {
        transform.rotation = Quaternion.identity;
    }

    #endregion

    #region Public Behaviour

    public void DisableRoutine () {
        StopAllCoroutines();
        anim.Play("Disable");
        explosion.transform.position = transform.position;
        explosion.Play();
        EnemyHitEvent.Invoke();
    }

    public void Init (Enemy enemy) {
        this.enemy = enemy;
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

    #endregion

}
