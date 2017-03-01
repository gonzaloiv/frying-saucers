using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEvasionBehaviour : MonoBehaviour {

  #region Fields

  private PlayerBehaviour playerBehaviour;

  #endregion

  #region Mono Behaviour

  void Awake() {
    playerBehaviour = GetComponent<PlayerBehaviour>();
  }

  void OnParticleCollision(GameObject particle) {
    PlayerMove(particle);
  }

  #endregion


  #region Private Behaviour

  private void PlayerMove(GameObject obj) {
    if (obj.layer == (int) CollisionLayer.Enemy) {
      Vector2 distance = obj.transform.position - transform.position;
      PlayerBehaviourPositions.AddPosition((Vector2) transform.position + distance.normalized);
      playerBehaviour.SetNextPosition();
    }
  }

  #endregion

}