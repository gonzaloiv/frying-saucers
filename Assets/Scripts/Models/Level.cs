using UnityEngine;
using System.Collections.Generic;
using Models;

namespace Models {

  public struct Level {

    #region Fields

    public int LevelNumber { get { return levelNumber; } }
    private int levelNumber;

    public List<Wave> Waves { get { return waves; } }
    private List<Wave> waves;

    public Vector2 PlayerPosition { get { return playerPosition; } }
    private Vector2 playerPosition;

    private int currentWave;

    #endregion

    #region Public Behaviour

    public Level(int levelNumber, Vector2 playerPosition, List<Wave> waves) {
      this.levelNumber = levelNumber;
      this.playerPosition = playerPosition;
      this.waves = waves;
      this.currentWave = 0;
    }

    public Wave CurrentWave() {
      Wave wave = waves[currentWave];
//    currentWave++; => Always same wave for debug
      return wave;
    }

    public bool HasMoreWaves() {
      return currentWave < waves.Count;
    }

    #endregion
	
  }

}