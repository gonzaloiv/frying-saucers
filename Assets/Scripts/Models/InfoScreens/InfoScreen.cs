using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable] public class InfoScreen {

    #region Fields / Properties

    [SerializeField] private InfoScreenData infoScreenData;
    [SerializeField] private UnityEvent onLastInfoScreen;

    public List<string> CurrentInfoScreenText { get { return infoScreenData.Texts[currentInfoScreenIndex].Lines; } }
    public bool IsLastInfoScreenText { get { return currentInfoScreenIndex == infoScreenData.Texts.Count - 1; } } 
    private int currentInfoScreenIndex = 0;

    #endregion

    #region Public Behaviour

    public void ResetInfoScreenIndex(){
        currentInfoScreenIndex = 0;
    }

    public void IncreaseInfoScreenIndex(){
        currentInfoScreenIndex++;
    }

    #endregion

}
