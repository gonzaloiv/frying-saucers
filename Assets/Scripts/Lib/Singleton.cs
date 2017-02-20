using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credits: http://gamelogic.co.za/unity-extensions/
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

  #region Fields

  protected static T instance;

  #endregion

  #region  Singleton Pattern

  public static T Instance {
    get {
      if (instance == null) {
        instance = (T) FindObjectOfType(typeof(T));
        if (instance == null)
          Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
      }
      return instance;
    }
  }

  #endregion

}

