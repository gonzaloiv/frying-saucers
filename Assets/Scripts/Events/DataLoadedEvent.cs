using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadedEvent : EventArgs {
    public DataLoadedEvent () {
        Debug.Log("DataLoadedEvent");
    }
}