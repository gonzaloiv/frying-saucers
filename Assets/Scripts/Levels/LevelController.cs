using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

  #region Fields

  private int currentLevelNumber = 1;
  private Level currentLevel;

  #endregion

  #region Public Behaviour

  public void Initialize() {
    currentLevel = new Level(currentLevelNumber);
    Debug.Log("Level " + currentLevel.LevelNumber);
  }

  #endregion
	
}
