using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEvasionBehaviour : MonoBehaviour {

  #region Fields

  private static Vector2 INITIAL_POSITION = Vector2.zero;
  private const int MAX_POSITIONS = 10;
  private const float MAX_SPEED = 0.1f;
  private const int INITIAL_ROW = 3;

  private List<Vector2> positions = new List<Vector2>();
  private Vector2 nextPosition = Vector2.zero;

  private int lastCollision;

  #endregion

  #region Mono Behaviour

  void Update() {
	  transform.position = Vector2.Lerp(transform.position, INITIAL_POSITION, MAX_SPEED / 3f);
    transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED);
  }

  void OnEnable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
    StartCoroutine(MovementRoutine());
  }

  void OnDisable() {
    EventManager.StopListening<EnemyShotEvent>(OnEnemyShotEvent);
    StopAllCoroutines();
  }

  void OnParticleCollision(GameObject particle) {
    if (particle.layer == (int) CollisionLayer.Enemy) {

      positions.Add(particle.transform.position);
      if (positions.Count > MAX_POSITIONS)
        positions.Remove(positions[0]);

      int currentCollision = particle.gameObject.GetInstanceID();
      if(currentCollision != lastCollision)
        nextPosition = GetValidPosition();

      lastCollision = currentCollision;
    }
  }

  #endregion

  #region Event Behaviour

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    positions.Add(transform.position);
    if (positions.Count > MAX_POSITIONS)
      positions.Remove(positions[0]);
  }

  #endregion

  #region Private Behaviour

  private IEnumerator MovementRoutine() {
    while(gameObject.activeSelf) {
      if(positions.Where(x => x == (Vector2) transform.position).Count() > 0)
        nextPosition = GetValidPosition();
      yield return new WaitForSeconds(0.001f);
    }
  }

  private Vector2 GetValidPosition() {

    List<Vector2> testedPositions = new List<Vector2>();      
    Vector2 testPosition;
    Vector2 newPosition = Vector2.one;
    int row = INITIAL_ROW;

    while (newPosition == Vector2.one) {

      testedPositions = new List<Vector2>();
      row++;

      while (testedPositions.Count() < 8 && newPosition == Vector2.one) {
        testPosition = (Vector2) transform.position + IncrementInsideBoard(row);
        if (positions.Contains(testPosition)) {
          testedPositions.Add(testPosition);
        } else {
          newPosition = testPosition;
        }
      }

      newPosition = newPosition == Vector2.one ? INITIAL_POSITION : newPosition; // In case there's no space empty...

    }

    return newPosition;
   
  }

  private Vector2 IncrementInsideBoard(int row) {

    Vector2 increment = new Vector2(new int[] {-row, row}[Random.Range(0, 2)] , new int[] {-row, row}[Random.Range(0, 2)]);

    if(transform.position.x + increment.x <= -Board.BoardSize.x / 2 + row) // X MIN
      increment = new Vector2(new int[] {0, row}[Random.Range(0, 2)] , new int[] {-row, row}[Random.Range(0, 2)]);
    if(transform.position.x + increment.x >= Board.BoardSize.x / 2 - row) // X MAX
      increment = new Vector2(new int[] {-row, 0}[Random.Range(0, 2)] , new int[] {-row, row}[Random.Range(0, 2)]);
    if(transform.position.y + increment.y <= -Board.BoardSize.y / 2 + row) // Y MIN
      increment = new Vector2(new int[] {-row, row}[Random.Range(0, 2)] , new int[] {0, row}[Random.Range(0, 2)]);
    if(transform.position.y + increment.y >= Board.BoardSize.y / 2 - row) // Y MAX
      increment = new Vector2(new int[] {-row, row}[Random.Range(0, 2)] , new int[] {-row, 0}[Random.Range(0, 2)]);

    return increment;

  }

  #endregion
	
}