using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

  #region Mono Behaviour

  void OnEnable() {
    EventManager.StartListening<ClickInput>(OnClickInput);
  }

  void OnDisable() {
    EventManager.StopListening<ClickInput>(OnClickInput);
  }

  #endregion

  #region Event Behaviour

  void OnClickInput(ClickInput clickInput) {
    RaycastHit2D hit = Physics2D.Raycast(transform.position, clickInput.Position, Vector2.Distance(transform.position, clickInput.Position));
    if (hit.collider != null && hit.collider.gameObject.layer == (int) CollisionLayer.Enemy)
      hit.collider.transform.GetComponent<UFO>().Disable();
  }

  #endregion

}
