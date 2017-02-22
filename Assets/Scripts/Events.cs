using UnityEngine;
using UnityEngine.Events;

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

#endregion

#region Game Mechanics Events 

public class NewLevelEvent : UnityEvent {
  public NewLevelEvent() {
    Debug.Log("NewLevelEvent");
  }
}

public class EnemyHitEvent : UnityEvent {

  public int Score { get { return score; } }
  private int score;

	public EnemyHitEvent(int score) {
    this.score = score;
  }

}

public class GameOverEvent : UnityEvent {
  public GameOverEvent() {
    Debug.Log("GameOverEvent");
  }
}

#endregion

#region UI Events 
#endregion