using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadedEventArgs : EventArgs {

    #region Fields / Properties

    public int TotalPlaysAmount { get { return totalPlaysAmount; } }
    private int totalPlaysAmount;

    #endregion

    #region Public Behaviour

    public DataLoadedEventArgs (int totalPlaysAmount) {
        this.totalPlaysAmount = totalPlaysAmount;
    }

    #endregion

}