using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level {

  #region Fields

  public int LevelNumber { get { return levelNumber; } }
  private int levelNumber;

  public List<Wave> Waves { get { return waves; } }
  private List<Wave> waves;

  private int currentWave;

  #endregion 

  #region Public Behaviour

  public Level(int levelNumber, List<Wave> waves) {
    this.levelNumber = levelNumber;
    this.waves = waves;
    this.currentWave = 0;
  }

  public Wave CurrentWave() {
    Wave wave = waves[currentWave];
    currentWave++;
    return wave;
  }

  public bool HasMoreWaves() {
    return currentWave < waves.Count;
  }

  #endregion
	
}
