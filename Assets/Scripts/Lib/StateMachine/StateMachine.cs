using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

  #region Fields

  public virtual State CurrentState { get { return currentState; } }
  private State currentState;

  #endregion

  #region Public Behaviour

  public virtual State GetState<T>() where T : State {
    T state = GetComponent<T>();
    if (state == null)
      state = gameObject.AddComponent<T>();
    return state;
  }

  public virtual void ChangeState<T>() where T : State {
    if (currentState != null)
      currentState.Exit();
    currentState = GetState<T>();
    if (currentState != null)
      currentState.Enter();
  }

  #endregion

}