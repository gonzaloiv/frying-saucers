using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehaviour01 : MonoBehaviour, IEnemyBehaviour {

  #region Fields

  [SerializeField] private GameObject laserPrefab;
  private ParticleSystem laser;

  private GameObject player;

  #endregion

  #region Mono Behaviour

  void Awake() {
	laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
  }

  void Update() {
    if (transform.position.y > -10)
      transform.Translate(Vector2.down * Config.GRAVITY * Time.deltaTime, Space.World);
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region IEnemyBehaviour

  public void Initialize(GameObject player) {
    this.player = player;
  }

  public void Play() {
    StartCoroutine(ShootingRoutine());
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ShootingRoutine() {
    while(gameObject.activeSelf) {
      EventManager.TriggerEvent(new EnemyShotEvent(transform.position));
      transform.rotation = QuaternionToPlayer();
      laser.Play();
      yield return new WaitForSeconds(1f);
    }
  }

  private Quaternion QuaternionToPlayer() {

    Quaternion quaternion = Quaternion.identity;
    Vector2 moveDirection = (Vector2) transform.position - (Vector2) player.transform.position; 

    if (moveDirection != Vector2.zero) {
      float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
      quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    return quaternion;

  }

  #endregion
	
}
