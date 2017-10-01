using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreditsEvent : EventArgs {
    public CreditsEvent () {
        Debug.Log("CreditsEvent");
    }
}