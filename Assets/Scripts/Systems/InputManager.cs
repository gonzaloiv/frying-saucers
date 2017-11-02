using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    #region Events

    public delegate void EscapeInputEventHandler ();
    public static event EscapeInputEventHandler EscapeInputEvent = delegate {};

    public delegate void TapInputEventHandler ();
    public static event TapInputEventHandler TapInputEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            EscapeInputEvent.Invoke();
        if (Input.GetMouseButtonDown(0))
            TapInputEvent.Invoke();
    }

    #endregion

}