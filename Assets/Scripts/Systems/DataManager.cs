using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataManager {

    #region Fields

    private const string USER_DATA_FILE_NAME = "UserData.binary";

    public static UserData UserData { get { return userData; } }
    private static UserData userData;

    #endregion

    #region Events

    public delegate void DataLoadedEventHandler (DataLoadedEventArgs dataLoadedEventArgs);
    public static event DataLoadedEventHandler DataLoadedEvent = delegate {};

    #endregion

    #region Public Behaviour

    public static void Init () {
        
        LoadData();
    }

    public static void SetNewScore (int newScore) { 
        for (int i = 0; i < userData.LeaderboardEntries.Length; i++) {
            if (newScore > userData.LeaderboardEntries[i].Score) {
                for (int j = userData.LeaderboardEntries.Length; j < i; i--)
                    userData.LeaderboardEntries[j] = userData.LeaderboardEntries[j - 1];
                userData.LeaderboardEntries[i].Score = newScore;
                userData.LeaderboardEntries[i].Date = DateTime.Now;
                break;
            }
        }
        SaveData();
    }

    public static void IncreaseUserDataTotalPlaysAmount () {
        userData.IncreaseTotalPlaysAmount();
        SaveData();
    }

    #endregion

    #region Private Behaviour

    private static void LoadData () {
        userData = new UserData();
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open(Path.Combine(Application.persistentDataPath, USER_DATA_FILE_NAME), FileMode.Open);
            userData = (UserData) formatter.Deserialize(saveFile);
            saveFile.Close();
        } catch (Exception exception) {
            Debug.Log(exception.Message);
        }
        DataLoadedEvent.Invoke(new DataLoadedEventArgs(userData.TotalPlaysAmount));
        Debug.Log("UserData.TotalPlaysAmount " + userData.TotalPlaysAmount);
    }

    private static void SaveData () {
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Create(Path.Combine(Application.persistentDataPath, USER_DATA_FILE_NAME));
            formatter.Serialize(saveFile, userData);
            saveFile.Close();
        } catch (Exception exception) {
            Debug.Log(exception.Message);
        }
    }

    #endregion

}
