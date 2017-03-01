using UnityEngine;
using System.Collections.Generic;
using Models;

namespace Models {

  public struct Level {

    #region Fields

    public int LevelNumber { get { return levelNumber; } }
    private int levelNumber;

    public int Waves { get { return waves; } }
    private int waves;

    private int currentWave;

    #endregion

    #region Public Behaviour

    public Level(int levelNumber, int waves) {
      this.levelNumber = levelNumber;
      this.waves = waves;
      this.currentWave = 0;
    }

    public int CurrentWave() {
      currentWave++;
      return currentWave;
    }

    public bool HasMoreWaves() {
      return currentWave < waves;
    }

    #endregion
	
  }

}