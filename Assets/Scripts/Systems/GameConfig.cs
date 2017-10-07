using UnityEngine;
using System.Collections.Generic;

public class GameConfig {

    #region Fields / Properties

    // Enemies
    public static float EnemyMaxSpeed = 10f;
    public static int EnemyScore = 10;
    public static float EnemyInitialYPosition = 3.5f;

    // Player
    public static float PlayerInitialYPosition = -1f;

    // Waves
    public static int RandomWaveEnemyAmount = 4;

    // Game
    public static int GameTimeScale = 1;
    public static int ShootingRoutineSections = 6;

    // Input
    public static float GestureStrokeTime = 0.5f;
    public static float GestureMinScore = 0.85f;

    #endregion

    #region Mono Behaviour

    public static void Init (GameConfigData gameConfigData) {
        EnemyMaxSpeed = gameConfigData.EnemyMaxSpeed;
        EnemyScore = gameConfigData.EnemyScore;
        EnemyInitialYPosition = gameConfigData.EnemyInitialYPosition;
        RandomWaveEnemyAmount = gameConfigData.RandomWaveEnemyAmount;
        GameTimeScale = gameConfigData.GameTimeScale;
        ShootingRoutineSections = gameConfigData.ShootingRoutineSections;
        GestureStrokeTime = gameConfigData.GestureStrokeTime;
        GestureMinScore = gameConfigData.GestureMinScore;
    }

    #endregion


}