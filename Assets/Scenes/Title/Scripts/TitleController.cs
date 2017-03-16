using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

  #region Fields

  [SerializeField] private int openingScene;
  [SerializeField] private int tutorialScene;

  #endregion

  #region Public Behaviour

  public void LoadNextScene() {
    if(DataManager.GetIsTutorialPlayed())
      SceneManager.LoadScene(openingScene);
    else
      SceneManager.LoadScene(tutorialScene);
  }

  #endregion

}
