using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level {

  public int LevelNumber { get { return levelNumber; } }
  private int levelNumber;

  public Level(int levelNumber) {
    this.levelNumber = levelNumber;
  }
	
}
