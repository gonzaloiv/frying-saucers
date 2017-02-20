using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  #region Mono Behaviour

  void Update() {

    if (Time.timeScale != 0) {

      if (Input.GetKeyDown(KeyCode.Escape))
        EventManager.TriggerEvent(new EscapeInput());

      if (Input.GetKeyDown(KeyCode.Return))
        EventManager.TriggerEvent(new ReturnInput());
    }
   
  }

  #endregion

}