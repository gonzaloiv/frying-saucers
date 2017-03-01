using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class UFOController : MonoBehaviour, IEnemyController {

  #region Fields

  [SerializeField] private GameObject explosionPrefab;
  private ParticleSystem explosion;

  private Animator animator;

  private EnemyType enemyType;
  private bool activeCollider; // TODO: repensar como controlar sólo una colisión por grupo de partículas

  #endregion

  #region Mono Behaviour

  void Awake() {
    explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    animator = GetComponent<Animator>();
  }

  void OnEnable() {
    activeCollider = true;
    EventManager.StartListening<GestureInput>(OnGestureInput);
  }

  void OnDisable() {
    EventManager.StopListening<GestureInput>(OnGestureInput);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer == (int) CollisionLayer.Board)
      Disable();
    if (collision.gameObject.layer == (int) CollisionLayer.Player) {
      animator.Play("Disable");
      explosion.Play();
    }
  }

  void OnParticleCollision(GameObject particle) {
    if (activeCollider && particle.layer == (int) CollisionLayer.Player) {
      animator.Play("Disable");
      explosion.Play();
      activeCollider = false;
      EventManager.TriggerEvent(new EnemyHitEvent((int) EnemyScore.UFO));
    }
  }
  #endregion

  #region Event Behaviour

  void OnGestureInput(GestureInput gestureInput) {
    if((int) gestureInput.Type == (int) enemyType) {
      animator.Play("Disable");
      explosion.Play();
      EventManager.TriggerEvent(new EnemyHitEvent((int) EnemyScore.UFO));
    }
  }

  #endregion

  #region Public Behaviour

  public void Initialize(EnemyType enemyType) {
    this.enemyType = enemyType;
  }

  public void Disable() {
    gameObject.SetActive(false);
  }

  #endregion

}
