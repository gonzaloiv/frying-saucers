using UnityEngine;
using UnityEngine.Events;
using PDollarGestureRecognizer;

#region Player Input Events

public class EscapeInput : UnityEvent {}

public class GestureInput : UnityEvent {

  public GestureType Type { get { return type; } }
  private  GestureType type;

  public float Score { get { return score; } }
  private float score;

  public GestureTime Time { get { return time; } }
  private GestureTime time;

  public GestureInput(string gestureClass, float score, GestureTime time) {

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
    this.time = time;

  }

}

public class RightGestureInput : UnityEvent {

  public GestureInput GestureInput { get { return gestureInput; } }
  private GestureInput gestureInput;

  public RightGestureInput(GestureInput gestureInput) {
    this.gestureInput = gestureInput;
    Debug.Log("RightGestureInput " + gestureInput.Time.ToString());
  }

}

public class WrongGestureInput : UnityEvent {

  public GestureInput GestureInput { get { return gestureInput; } }
  private GestureInput gestureInput;

  public WrongGestureInput(GestureInput gestureInput) {
    this.gestureInput = gestureInput;
    Debug.Log("WrongGestureInput");
  }

}

#endregion

#region Game Mechanics Events 

public class EnemyAttackEvent : UnityEvent {

  public EnemyType EnemyType { get { return enemyType; } }
  private EnemyType enemyType;

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public float RoutineTime { get { return routineTime; } }
  private float routineTime;

  public float SectionTime { get { return sectionTime; } }
  private float sectionTime;

  public EnemyAttackEvent(EnemyType enemyType, Vector2 position, float routineTime) {
    this.enemyType = enemyType;
    this.position = position;
    this.routineTime = routineTime;
    this.sectionTime = routineTime / Config.SHOOTING_ROUTINE_PARTS;
  }

}

public class EnemyShotEvent : UnityEvent {

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public EnemyShotEvent(Vector2 position) {
    this.position = position;
  }

}

public class EnemyHitEvent : UnityEvent {
  public EnemyHitEvent() {
    Debug.Log("EnemyHitEvent");
  }
}

public class PlayerHitEvent : UnityEvent {
  public PlayerHitEvent() {
    Debug.Log("PlayerHitEvent");
  }
}

#endregion

#region Level Events 

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

public class GameOverEvent : UnityEvent {

  public int Score { get { return score; } } 
  private int score;

  public GameOverEvent(int score) {
    this.score = score;
    Debug.Log("GameOverEvent " + this.score);
  }

}

#endregion