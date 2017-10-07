using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHitEventArgs : EventArgs {
    public PlayerHitEventArgs () {
        Debug.Log("PlayerHitEvent");
    }
}