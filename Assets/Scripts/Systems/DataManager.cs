using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataManager {

    #region Fields

    private const string USER_DATA = "UserData";

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

    public static void AddNewScore (LeaderboardEntry leaderboardEntry) { 
        userData.AddNewScore(leaderboardEntry);
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
        UserData persistedUserData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(USER_DATA));
        if (persistedUserData != null) {
            userData.SetTotalPlaysAmount(persistedUserData.TotalPlaysAmount);
            if (persistedUserData.LeaderboardEntries != null)
                userData.SetLeaderboardEntries(persistedUserData.LeaderboardEntries);
        }
        DataLoadedEvent.Invoke(new DataLoadedEventArgs(userData.TotalPlaysAmount));
        Debug.Log("UserData.TotalPlaysAmount " + userData.TotalPlaysAmount);
    }

    private static void SaveData () {
        PlayerPrefs.SetString(USER_DATA, JsonUtility.ToJson(userData));
        PlayerPrefs.Save();
        Debug.Log("UserData saved correctly.");
    }

    #endregion

}