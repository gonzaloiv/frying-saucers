using UnityEngine;

public class InputManager : MonoBehaviour {

  #region Fields

  private Camera camera;

  private float temps = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
    camera = GameObject.FindObjectOfType<Camera>();
  }

  void Update() {

    if (Time.timeScale != 0) {

      if (Input.GetMouseButtonDown(0)) {
        temps = Time.time;
        EventManager.TriggerEvent(new ClickInput(camera.ScreenToWorldPoint(Input.mousePosition)));
      }

      if (Input.GetMouseButtonUp(0) && Time.time - temps >= 0.6f)
        EventManager.TriggerEvent(new LongClickInput(camera.ScreenToWorldPoint(Input.mousePosition)));

    }

//    if (Input.GetKeyDown(KeyCode.Return))
//        EventManager.TriggerEvent(new ReturnInput());
//
//    if (Input.GetKeyDown(KeyCode.Escape))
//      EventManager.TriggerEvent(new EscapeInput());

  }

  #endregion

}


