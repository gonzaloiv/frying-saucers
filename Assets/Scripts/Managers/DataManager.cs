using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using Models;

public class DataManager : MonoBehaviour {

  #region Fields

  private static Leaderboard leaderboard;
  private string dataPath;

  #endregion

  #region Mono Behaviour

  void Awake() {
    dataPath = Application.persistentDataPath;
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

  private void SetNewScore(int newScore) {
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

  private void SaveData() {

    BinaryFormatter formatter = new BinaryFormatter();
    FileStream saveFile = File.Create(dataPath + "/scores.binary");

    formatter.Serialize(saveFile, leaderboard);

    saveFile.Close();

  }

  private void LoadData() {
    try {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream saveFile = File.Open(dataPath + "/scores.binary", FileMode.Open);

      leaderboard = (Leaderboard) formatter.Deserialize(saveFile);
        
      saveFile.Close();

      for (int i = 0; i < leaderboard.Scores.Length; i++) {
        Debug.Log(leaderboard.Scores[i]);
        Debug.Log(leaderboard.Dates[i]);
      }
    } catch (FileNotFoundException exception) {
      Debug.Log("First play: Data not recorded, yet");
    }
  }

}
