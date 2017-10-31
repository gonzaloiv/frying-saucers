using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadedEventArgs : EventArgs {

    public int TotalPlaysAmount { get { return totalPlaysAmount; } }
    private int totalPlaysAmount;

    public DataLoadedEventArgs (int totalPlaysAmount) {
        this.totalPlaysAmount = totalPlaysAmount;
    }

}