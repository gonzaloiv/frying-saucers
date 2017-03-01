using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Models;

public class PlayerBehaviour02 : MonoBehaviour {

  #region Fields

  public static Vector2 INITIAL_POSITION = Config.PLAYER_INITIAL_POSITION;
  public const float MAX_SPEED = 0.06f;
  public const int INITIAL_ROW = 2;

  private Animator anim;

  private List<GameObject> enemies; // TODO: limpiar esto si se confirma que no hace falta
  private Vector2 nextPosition = INITIAL_POSITION;

  private bool returning = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
  }

  void Update() {
    if (!returning) {
      transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED * Time.timeScale);
    } else {
      transform.position = Vector2.Lerp(transform.position, INITIAL_POSITION, MAX_SPEED * Time.timeScale);
      transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
  }

  void OnEnable() {
    StartCoroutine(ReturnToInitialPosition());
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Initialize(List<GameObject> enemies) {
    this.enemies = enemies;
  }

  #endregion

  #region Public Behaviour

  public void SetNextPosition() {
    if(Moving())
      return;

    nextPosition = PositionInsideBoard(INITIAL_ROW);

  }

  #endregion

  #region Private Behaviour

  private Vector2 PositionInsideBoard(int row) {

    Vector2 position = transform.position;

    if (transform.position.x <= -Board.BOARD_SIZE.x) { 
      position += new Vector2(-transform.position.x, 0);
    } else if (transform.position.x >= Board.BOARD_SIZE.x) {
      position += new Vector2(-transform.position.x, 0);
    } else if (transform.position.y <= -Board.BOARD_SIZE.y) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    } else if (transform.position.y >= 0) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    } else {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    }

    return position;
      
  }

  private IEnumerator ReturnToInitialPosition() {
    while (gameObject.activeSelf) {
      yield return new WaitForSeconds(Random.Range(3f, 4f));
      if (transform.position.x <= -Board.BOARD_SIZE.x || transform.position.x >= Board.BOARD_SIZE.x) {
        anim.Play("Return");
        returning = true;
      }
      yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));
      transform.rotation = Quaternion.identity;
      returning = false;
    }
  }

  private bool Moving() {
    return (nextPosition - (Vector2) transform.position).sqrMagnitude > 1f;
  }

  #endregion

}
