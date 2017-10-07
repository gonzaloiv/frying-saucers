using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHitEventArgs : EventArgs {
    public EnemyHitEventArgs () {
        Debug.Log("EnemyHitEvent");
    }
}