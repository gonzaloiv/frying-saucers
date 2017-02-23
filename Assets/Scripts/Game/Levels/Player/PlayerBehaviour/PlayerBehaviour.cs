using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour {

  #region Fields

  public static Vector2 INITIAL_POSITION = Vector2.zero;
  public const float MAX_SPEED = 0.06f;
  public const int INITIAL_ROW = 2;

  private List<GameObject> enemies; // TODO: limpiar esto si se confirma que no hace falta
  private Vector2 nextPosition = Vector2.zero;

  #endregion

  #region Mono Behaviour

  void Update() {
    transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED);
  }

  #endregion

  #region Public Behaviour

  public void Initialize(List<GameObject> enemies) {
    this.enemies = enemies;
  }

  #endregion

  #region Public Behaviour

  public void SetNextPosition() {

    List<Vector2> testedPositions = new List<Vector2>();      
    Vector2 testPosition;
    Vector2 newPosition = Vector2.one;
    int row = INITIAL_ROW;

    while (newPosition == Vector2.one) {

      testedPositions = new List<Vector2>();
      row++;

      while (testedPositions.Count() < 8 && newPosition == Vector2.one) {

        testPosition = PositionInsideBoard(row);

        bool occupied = Physics2D.OverlapCircle(testPosition, 2);

        if (PlayerBehaviourPositions.Positions.Contains(testPosition) || occupied) {
          testedPositions.Add(testPosition);
        } else {
          newPosition = testPosition;
        }
      }

      nextPosition = newPosition == Vector2.one ? INITIAL_POSITION : newPosition; // In case there's no space empty...
    }
  }

  #endregion

  #region Private Behaviour

  private Vector2 PositionInsideBoard(int row) {

    // TODO: ANIMACIÓN PARA CUANDO VUELVE AL MEDIO
    Vector2 position = transform.position;

    if (transform.position.x <= -Board.BOARD_SIZE.x) { 
      position += new Vector2(-transform.position.x, new int[] { -row, row }[Random.Range(0, 2)] / 2);
    } else if (transform.position.x >= Board.BOARD_SIZE.x) {
      position += new Vector2(-transform.position.x, new int[] { -row, row }[Random.Range(0, 2)] / 2);
    } else if (transform.position.y <= -Board.BOARD_SIZE.y) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], -transform.position.y);
    }else if (transform.position.y >= 0) {
      position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], -transform.position.y);
    }else {
     position += new Vector2(new int[] { -row, row }[Random.Range(0, 2)], new int[] { -row, row }[Random.Range(0, 2)] / 2);
    }

    return position;

  }


  #endregion

}
