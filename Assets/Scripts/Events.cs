using UnityEngine.Events;
using UnityEngine;

#region Player Input Events

public class EscapeInput : UnityEvent {}
public class ReturnInput : UnityEvent {}

#endregion

#region Game Mechanics Events 

public class EnemyHitEvent : UnityEvent {
	public EnemyHitEvent() {
		Debug.Log("EnemyHitEvent");
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

