using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHitEvent : EventArgs {
    public EnemyHitEvent () {
        Debug.Log("EnemyHitEvent");
    }
}