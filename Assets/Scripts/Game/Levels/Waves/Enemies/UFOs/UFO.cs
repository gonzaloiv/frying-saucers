using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour, IEnemy {

  #region Fields

  [SerializeField] private GameObject explosionPrefab;
  private ParticleSystem explosion;

  private Animator animator;
  private IEnemyBehaviour behaviour;

  private bool activeCollider;
  private const int score = 10;

  #endregion

  #region Mono Behaviour

  void Awake() {
    explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    animator = GetComponent<Animator>();
    behaviour = GetComponent<IEnemyBehaviour>();
  }

  void OnEnable() {
    activeCollider = true;
  }

  void OnParticleCollision(GameObject particle) {
    if (activeCollider && particle.layer == (int) CollisionLayer.Player) {
      animator.Play("Disable");
      explosion.Play();
      activeCollider = false;
      EventManager.TriggerEvent(new EnemyHitEvent(score));
    }
  }

  #endregion

  #region Public Behaviour
  
  public void Disable() {
    gameObject.SetActive(false);
  }

  #endregion

}
