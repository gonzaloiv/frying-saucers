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

  void OnEnable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  void OnDisable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  #endregion


  #region Private Behaviour

  private void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    Vector2 distance = enemyShotEvent.Position - (Vector2) transform.position;
    PlayerBehaviourPositions.AddPosition((Vector2) transform.position + distance.normalized);
    playerBehaviour.SetNextPosition();
  }

  #endregion

}
