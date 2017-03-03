using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemyBehaviour {

  #region Fields

  [SerializeField] private GameObject laserPrefab;
  private ParticleSystem laser;

  private Animator animator;
  private GameObject player;
  private EnemyController enemyController;

  bool playing = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    animator = GetComponent<Animator>();
  	laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
    enemyController = GetComponent<EnemyController>();
  }

  void Update() {
    if(playing)
      transform.position = Vector2.Lerp(transform.position, BoardManager.ENEMY_SHOT_POSITION, Config.ENEMY_MAX_SPEED * Time.timeScale);
    else
      transform.position = Vector2.Lerp(transform.position, enemyController.Enemy.Position, Config.ENEMY_MAX_SPEED * Time.timeScale);
  }

  void OnEnable() {
    EventManager.StartListening<GestureInput>(OnGestureInput);
  }

  void OnDisable() {
    EventManager.StopListening<GestureInput>(OnGestureInput);
    StopAllCoroutines();
  }

  #endregion

  #region Event Behaviour

  void OnGestureInput(GestureInput gestureInput) {
    if (playing)
      if ((int) gestureInput.Type == (int) enemyController.Enemy.EnemyType)
        enemyController.DisableRoutine();
  }

  #endregion

  #region IEnemyBehaviour

  public void Initialize(GameObject player) {
    this.player = player;
    playing = false;
  }

  public void Play() {
    StartCoroutine(ShootingRoutine());
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ShootingRoutine() { // TODO: Refact. this
    playing = true;
    EventManager.TriggerEvent(new EnemyShotEvent(enemyController.Enemy.EnemyType));
    yield return new WaitForSeconds(0.2f);
    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Disable")) {
      animator.Play("Shooting");
      yield return new WaitForSeconds(1.2f);
      transform.rotation = QuaternionToPlayer();
      laser.Play();
      yield return new WaitForSeconds(0.8f);
    }
    animator.Play("Idle");
    playing = false;
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
