using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShieldBehaviour : MonoBehaviour {

  #region Fields

  private const int SHIELD_AMOUNT = 6;

  [SerializeField] private GameObject shieldPrefab;
  private List<ParticleSystem> shields = new List<ParticleSystem>();

  [SerializeField] private GameObject explosionPrefab;
  private ParticleSystem explosion;

  private int lastCollision;

  #endregion

  #region Mono Behaviour

  void Awake() {
    for(int i = 0; i < SHIELD_AMOUNT; i++) 
      shields.Add(Instantiate(shieldPrefab, transform).GetComponent<ParticleSystem>());
    explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
  }

  void OnCollisionEnter2D(Collision2D collision2D) {
    if (collision2D.gameObject.layer == (int) CollisionLayer.Enemy) {
      Shield(collision2D.gameObject);
      explosion.transform.position = transform.position;
      explosion.Play();
    }
  }

  void OnParticleCollision(GameObject particle) {
    if (particle.layer == (int) CollisionLayer.Enemy) {
      int currentCollision = particle.gameObject.GetInstanceID();
      if (currentCollision != lastCollision)
        Shield(particle);
      lastCollision = currentCollision;
    }
  }

  #endregion

  #region Mono Behaviour

  private void Shield(GameObject obj) {
    Vector2 distance = obj.transform.position - transform.position;
    ParticleSystem shield = (ParticleSystem) shields.Where(x => !x.isPlaying).FirstOrDefault();
    if(shield != null) {
      shield.transform.position = (Vector2) transform.position + distance.normalized;
      shield.transform.rotation = Quaternion.AngleAxis(-90 + Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg, Vector3.forward);
      shield.Play();
    }
  }

  #endregion
  	
}