using UnityEngine;
using System.Collections.Generic;

public class GameConfig {

    #region Fields / Properties

    // Enemies
    public static float EnemyMaxSpeed;
    public static int EnemyScore;
    public static float EnemyInitialYPosition;

    // Player
    public static float PlayerInitialYPosition;
    public static float PlayerMaxSpeed;

    // Game
    public static int GameTimeScale;
    public static int ShootingRoutineSections;

    // Input
    public static float GestureStrokeTime;
    public static float GestureMinScore;

    #endregion

    #region Mono Behaviour

    public static void Init (GameConfigData gameConfigData) {
        EnemyMaxSpeed = gameConfigData.EnemyMaxSpeed;
        EnemyScore = gameConfigData.EnemyScore;
        EnemyInitialYPosition = gameConfigData.EnemyInitialYPosition;
        PlayerInitialYPosition = gameConfigData.PlayerInitialYPosition;
        PlayerMaxSpeed = gameConfigData.PlayerMaxSpeed;
        GameTimeScale = gameConfigData.GameTimeScale;
        ShootingRoutineSections = gameConfigData.ShootingRoutineSections;
        GestureStrokeTime = gameConfigData.GestureStrokeTime;
        GestureMinScore = gameConfigData.GestureMinScore;
        if (gameConfigData.ResetUserData)
            PlayerPrefs.DeleteAll();
    }

    #endregion


}