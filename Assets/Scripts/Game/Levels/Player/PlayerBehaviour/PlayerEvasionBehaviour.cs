using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEvasionBehaviour : MonoBehaviour {

  #region Fields

  private PlayerBehaviour playerBehaviour;
  private int previousCollision;

  #endregion

  #region Mono Behaviour

  void Awake() {
    playerBehaviour = GetComponent<PlayerBehaviour>();
  }

  void OnEnable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  void OnCollisionEnter(Collision collision) {
    PlayerMove(collision.gameObject);
  }

  void OnParticleCollision(GameObject particle) {
    PlayerMove(particle);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    PlayerBehaviourPositions.AddPosition(transform.position);
  }

  #endregion

  #region Private Behaviour

  private void PlayerMove(GameObject obj) {
    if (obj.layer == (int) CollisionLayer.Enemy) {
      int currentCollision = obj.GetInstanceID();
      if (currentCollision != previousCollision) { 
        Vector2 distance = obj.transform.position - transform.position;
        PlayerBehaviourPositions.AddPosition((Vector2) transform.position + distance.normalized);
        playerBehaviour.SetNextPosition();
      }
    }
  }

  #endregion

}