using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHitEvent : EventArgs {
    public PlayerHitEvent () {
        Debug.Log("PlayerHitEvent");
    }
}