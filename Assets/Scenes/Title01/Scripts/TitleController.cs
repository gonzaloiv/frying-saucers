using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

  #region Public Behaviour

  public void LoadMainScene() {
    SceneManager.LoadSceneAsync(1);
  }

  #endregion

}
