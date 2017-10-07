using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveEndEventArgs : EventArgs {
    public WaveEndEventArgs () {
        Debug.Log("WaveEndEvent");
    }
}