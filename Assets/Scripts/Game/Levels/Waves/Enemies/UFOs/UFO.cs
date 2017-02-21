using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour, IEnemy {

  #region Fields

  [SerializeField] private GameObject explosionPrefab;
  private ParticleSystem explosion;

  private Animator animator;
  private IEnemyBehaviour behaviour;

  #endregion

  #region Mono Behaviour

  void Awake() {
    explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
    animator = GetComponent<Animator>();
    behaviour = GetComponent<IEnemyBehaviour>();
  }

  void OnParticleCollision(GameObject particle) {
    if (particle.layer == (int) CollisionLayer.Player) {
      animator.Play("Disable");
      explosion.Play();
    }
  }

  #endregion

  #region Public Behaviour
 
  public void Disable() {
    gameObject.SetActive(false);
  }

  #endregion

}
