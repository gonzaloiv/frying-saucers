using UnityEngine;
using UnityEngine.Events;
using PDollarGestureRecognizer;

#region Player Input Events

public class EscapeInput : UnityEvent {}
public class ReturnInput : UnityEvent {}

public class GestureInput : UnityEvent {

  public GestureType Type { get { return type; } }
  private  GestureType type;

  public float Score { get { return score; } }
  private float score;

  public GestureInput(string gestureClass, float score) {

    Debug.Log("Gesture Input: " + gestureClass + " " + score);

    if (gestureClass.ToUpper() == GestureType.Circle.ToString().ToUpper()) {
      type = GestureType.Circle;
    } else if (gestureClass.ToUpper() == GestureType.Square.ToString().ToUpper()) {
      type = GestureType.Square;
    } else if (gestureClass.ToUpper() == GestureType.Triangle.ToString().ToUpper()) {
      type = GestureType.Triangle;
    } else if (gestureClass.ToUpper() == GestureType.Cross.ToString().ToUpper()) {
      type = GestureType.Cross;
    } else if (gestureClass.ToUpper() == GestureType.Victory.ToString().ToUpper()) {
      type = GestureType.Victory;
    }

    this.score = score;

  }

}

public class RightGestureInput : UnityEvent {

  public GestureInput GestureInput { get { return gestureInput; } }
  private GestureInput gestureInput;

  public RightGestureInput(GestureInput gestureInput) {
    Debug.Log("RightGestureInput");
    this.gestureInput = gestureInput;
  }

}

public class WrongGestureInput : UnityEvent {

  public GestureInput GestureInput { get { return gestureInput; } }
  private GestureInput gestureInput;

  public WrongGestureInput(GestureInput gestureInput) {
    Debug.Log("WrongGestureInput");
    this.gestureInput = gestureInput;
  }

}

#endregion

#region Game Mechanics Events 

public class NewGameEvent : UnityEvent {
  public NewGameEvent() {
    Debug.Log("NewGameEvent");
  }
}

public class NewLevelEvent : UnityEvent {
  public NewLevelEvent() {
    Debug.Log("NewLevelEvent");
  }
}

public class EnemyAttackEvent : UnityEvent {

  public EnemyType EnemyType { get { return enemyType; } }
  private EnemyType enemyType;

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public EnemyAttackEvent(EnemyType enemyType, Vector2 position) {
    this.enemyType = enemyType;
    this.position = position;
  }

}

public class EnemyShotEvent : UnityEvent {

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public EnemyShotEvent(Vector2 position) {
    this.position = position;
  }

}

public class GameOverEvent : UnityEvent {
  public GameOverEvent() {
    Debug.Log("GameOverEvent");
  }
}

#endregion

#region UI Events

public class EnemyHitEvent : UnityEvent {

  public EnemyHitEvent() {
    Debug.Log("EnemyHitEvent");
  }

}

#endregion