using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBehaviourPositions {

  #region Fields

  private const int MAX_POSITIONS = 3;

  public static List<Vector2> Positions { get { return positions; } }
  private static List<Vector2> positions = new List<Vector2>();

  #endregion

  #region Public Behaviour

  public static void AddPosition(Vector2 positions) {
    PlayerBehaviourPositions.positions.Add(positions);
    if (PlayerBehaviourPositions.positions.Count > MAX_POSITIONS)
      PlayerBehaviourPositions.positions.RemoveAt(0);
  }

  #endregion

}
