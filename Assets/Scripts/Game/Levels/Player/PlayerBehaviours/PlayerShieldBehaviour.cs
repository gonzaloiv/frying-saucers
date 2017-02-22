using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShieldBehaviour : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject shieldPrefab;
  private ParticleSystem shield;

  private int lastCollision;

  #endregion

  #region Mono Behaviour

  void Awake() {
    shield = Instantiate(shieldPrefab, transform).GetComponent<ParticleSystem>();
  }

  void OnParticleCollision(GameObject particle) {
    if (particle.layer == (int) CollisionLayer.Enemy) {
      int currentCollision = particle.gameObject.GetInstanceID();
      if (currentCollision != lastCollision) {
        Vector2 distance = particle.transform.position - transform.position;
        shield.transform.position = (Vector2) transform.position + distance.normalized;
        shield.transform.rotation = Quaternion.AngleAxis(-90 + Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg, Vector3.forward);
        shield.Play();
      }
      lastCollision = currentCollision;
    }
  }

  #endregion
  	
}