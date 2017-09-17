using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveEndEvent : EventArgs {
    public WaveEndEvent () {
        Debug.Log("WaveEndEvent");
    }
}