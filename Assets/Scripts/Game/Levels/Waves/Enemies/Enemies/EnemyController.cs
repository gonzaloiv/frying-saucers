using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class EnemyController : MonoBehaviour, IEnemyController {

  #region Fields

  [SerializeField] private GameObject explosionPrefab;
  private ParticleSystem explosion;

  private Animator animator;

  public Enemy Enemy { get { return enemy; } }
  private Enemy enemy;

  #endregion

  #region Mono Behaviour

  void Awake() {
    explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    animator = GetComponent<Animator>();
  }

  void OnEnable() {
    transform.rotation = Quaternion.identity;
  }

  #endregion

  #region Public Behaviour

  public void DisableRoutine() {
    animator.Play("Disable");
    explosion.Play();
    EventManager.TriggerEvent(new EnemyHitEvent((int) enemy.EnemyScore));
  }

  public void Initialize(Enemy enemy) {
    this.enemy = enemy;
  }

  public void Disable() {
    gameObject.SetActive(false);
  }

  public void StopBehaviour() {
    StopAllCoroutines();
  }

  #endregion

}
