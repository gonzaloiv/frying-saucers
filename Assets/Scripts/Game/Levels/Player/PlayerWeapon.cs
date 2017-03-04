using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject laserPrefab;
	private ParticleSystem laser;

  [SerializeField] private GameObject powerWavePrefab;
  private ParticleSystemController powerWaveController;

  #endregion

  #region Mono Behaviour

  void Awake() {
    laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
    powerWaveController = Instantiate(powerWavePrefab, transform.parent).GetComponent<ParticleSystemController>();
  }

  void Update() {
    transform.rotation = Quaternion.Slerp(transform.rotation, QuaternionToClick(), 1);
  }

  void OnEnable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
  }

  void OnDisable() {
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
  }

  #endregion

  #region Event Behaviour

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    laser.Play();
  }

  #endregion

  #region Private Behaviour

  private Quaternion QuaternionToClick() { 

    Quaternion quaternion = Quaternion.identity;
    Vector2 moveDirection = (Vector2) transform.position - BoardManager.ENEMY_SHOT_POSITION;

    if (moveDirection != Vector2.zero) {
      float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
      quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    return quaternion;
  }

  #endregion

}
