using UnityEngine;

public class InputManager : MonoBehaviour {

  #region Fields

  private Camera camera;

  #endregion
  
  #region Mono Behaviour

  void Awake() {
    camera = GameObject.FindObjectOfType<Camera>();
  }

  void Update() {

    if (Input.GetKeyDown(KeyCode.Escape))
      EventManager.TriggerEvent(new EscapeInput());

    if (Input.GetKeyDown(KeyCode.Return))
      EventManager.TriggerEvent(new ReturnInput());

    if (Input.GetMouseButtonDown(0)) 
      EventManager.TriggerEvent(new ClickInput(camera.ScreenToWorldPoint (Input.mousePosition)));

  }

  #endregion

}