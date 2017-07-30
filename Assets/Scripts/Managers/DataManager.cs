using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using Models;

public class DataManager : MonoBehaviour {

  #region Fields

  public static Leaderboard Leaderboard { get { return leaderboard; } }
  private static Leaderboard leaderboard;

  private static string dataPath;

  #endregion

  #region Mono Behaviour

  void Awake() {
    dataPath = Application.persistentDataPath;
    Debug.Log("Data: " + Application.persistentDataPath);
    leaderboard = new Leaderboard();
    LoadData();
  }

  void OnEnable() {
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    LoadData();
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    SetNewScore(gameOverEvent.Score);
    SaveData();
  }

  #endregion

  #region Public Behaviour

  public static void SetIsTutorialPlayed() {
    leaderboard.IsTutorialPlayer = true;
    SaveData();
  }

  public static bool GetIsTutorialPlayed() {
    return leaderboard.IsTutorialPlayer;
  }

  #endregion

  #region Private Behaviour

  private static void SetNewScore(int newScore) {
    for (int i = 0; i < leaderboard.Scores.Length; i++) {
      if (newScore > leaderboard.Scores[i]) {
        for (int j = leaderboard.Scores.Length; j < i; i--)
          leaderboard.Scores[j] = leaderboard.Scores[j - 1];
        leaderboard.Scores[i] = newScore;
        leaderboard.Dates[i] = DateTime.Now;
        break;
      }
    } 
  }

  private static void SaveData() {

    BinaryFormatter formatter = new BinaryFormatter();
    FileStream saveFile = File.Create(dataPath + "/leaderboard.binary");

    formatter.Serialize(saveFile, leaderboard);

    saveFile.Close();

  }

  private static void LoadData() {
    try {

      BinaryFormatter formatter = new BinaryFormatter();
      FileStream saveFile = File.Open(dataPath + "/leaderboard.binary", FileMode.Open);

      leaderboard = (Leaderboard) formatter.Deserialize(saveFile);
        
      saveFile.Close();

    } catch (FileNotFoundException exception) {
      Debug.Log(exception.Message);
      Debug.Log("First play: Data not recorded, yet");
    }
  }

  #endregion

}
