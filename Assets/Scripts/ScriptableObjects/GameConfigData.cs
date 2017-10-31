using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigData", menuName = "ScriptableObject/GameConfig", order = 1)]
public class GameConfigData : ScriptableObject {

    [Header("Enemies")]
    public float EnemyInitialYPosition = 3.5f;
    public float EnemyMaxSpeed = 10f;
    public int EnemyScore = 10;

    [Header("Player")]
    public float PlayerInitialYPosition = -1f;
    public float PlayerMaxSpeed = 10f;

    [Header("Waves")]    
    public int RandomWaveEnemyAmount = 4;

    [Header("Game")]
    public int GameTimeScale = 1;
    public int ShootingRoutineSections = 6;

    [Header("Input")]
    public float GestureStrokeTime = 0.5f;
    public float GestureMinScore = 0.85f;

    [Header("Launch Mode")]
    public bool ResetUserData = false;

}