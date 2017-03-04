using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Models;

public class PlayerBehaviour : MonoBehaviour {

  #region Fields

  public static Vector2 INITIAL_POSITION = Config.PLAYER_INITIAL_POSITION;
  public const float MAX_SPEED = 0.1f;
  public const int INITIAL_ROW = 2;
 
  private string[] animations = new string[] {"Return01", "Return02"};
  private Animator animator;

  private List<GameObject> enemies; // TODO: limpiar esto si se confirma que no hace falta
  private Vector2 nextPosition = INITIAL_POSITION;

  private bool returning = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    animator = GetComponent<Animator>();
  }

  void Update() {
    if(!Moving())
      nextPosition = INITIAL_POSITION;
    if(nextPosition == INITIAL_POSITION)
      transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED / 2 * Time.timeScale);
    else
      transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED * Time.timeScale);
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
    animator.Play(animations[Random.Range(0, animations.Length)]);
  }

  #endregion

  #region Private Behaviour

  private Vector2 PositionInsideBoard(int row) {

    Vector2 position = transform.position;

    if (transform.position.x <= -BoardManager.BOARD_SIZE.x) { 
      position += new Vector2(-transform.position.x, 0);
    } else if (transform.position.x >= BoardManager.BOARD_SIZE.x) {
      position += new Vector2(-transform.position.x, 0);
    } else if (transform.position.y <= -BoardManager.BOARD_SIZE.y) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    } else if (transform.position.y >= 0) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    } else {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], 0);
    }
          
    return position;
      
  }

  private bool Moving() {
    return (nextPosition - (Vector2) transform.position).sqrMagnitude > 0.4f;
  }

  #endregion

}
