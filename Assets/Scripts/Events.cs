using UnityEngine;
using UnityEngine.Events;
using PDollarGestureRecognizer;

#region Player Input Events

public class EscapeInput : UnityEvent {}
public class ReturnInput : UnityEvent {}

public class ClickInput : UnityEvent {

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public ClickInput(Vector2 position) {
    this.position = position;  
  }

}

public class LongClickInput : UnityEvent {

  public Vector2 Position { get { return position; } }
  private Vector2 position;

  public LongClickInput(Vector2 position) {
    this.position = position;  
  }

}

public class GestureInput : UnityEvent {

  public GestureType Type { get { return type; } }
  private  GestureType type;

  public GestureInput(Result result) {
    Debug.Log("Right: " + result.GestureClass + " " + result.Score);

    if (result.GestureClass.ToString().ToUpper() == GestureType.Circle.ToString().ToUpper()) {
      type = GestureType.Circle;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Square.ToString().ToUpper()) {
      type = GestureType.Square;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Triangle.ToString().ToUpper()) {
      type = GestureType.Triangle;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Cross.ToString().ToUpper()) {
      type = GestureType.Cross;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Victory.ToString().ToUpper()) {
      type = GestureType.Victory;
    }
  }

}

public class WrongGestureInput : UnityEvent {

  public GestureType Type { get { return type; } }
  private  GestureType type;

  public WrongGestureInput(Result result) {
    Debug.Log("Wrong: " + result.GestureClass + " " + result.Score);

    if (result.GestureClass.ToString().ToUpper() == GestureType.Circle.ToString().ToUpper()) {
      type = GestureType.Circle;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Square.ToString().ToUpper()) {
      type = GestureType.Square;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Triangle.ToString().ToUpper()) {
      type = GestureType.Triangle;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Cross.ToString().ToUpper()) {
      type = GestureType.Cross;
    } else if (result.GestureClass.ToString().ToUpper() == GestureType.Victory.ToString().ToUpper()) {
      type = GestureType.Victory;
    }
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

public class EnemyHitEvent : UnityEvent {

  public int Score { get { return score; } }
  private int score;

	public EnemyHitEvent(int score) {
    Debug.Log("EnemyHitEvent: " + score);
    this.score = score;
  }

}

public class EnemyShotEvent : UnityEvent {

  public EnemyType EnemyType { get { return enemyType; } }
  private EnemyType enemyType;

  public EnemyShotEvent(EnemyType enemyType) {
    this.enemyType = enemyType;
  }

}

public class GameOverEvent : UnityEvent {
  public GameOverEvent() {
    Debug.Log("GameOverEvent");
  }
}

#endregion