using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

  #region Fields

  [SerializeField] private int sceneToLoad;

  #endregion

  #region Public Behaviour

  public void LoadMainScene() {
    SceneManager.LoadSceneAsync(sceneToLoad);
  }

  #endregion

}
