using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInput {

    #region Fields / Properties

    public float InitialTime { get { return initialTime; } }
    public Vector2 VirtualKeyPosition { get { return virtualKeyPosition; } }
    public EnemyType EnemyType { get { return enemyType; } }
    public float SectionTime { get { return sectionTime; } }

    private float initialTime;
    private Vector2 virtualKeyPosition;
    private EnemyType enemyType;
    private float sectionTime;

    #endregion

    #region Public Behaviour

    public void SetInitialTime (float initialTime) {
        this.initialTime = initialTime;
    }

    public void SetVirtualKeyPosition (Vector2 virtualKeyPosition) {
        this.virtualKeyPosition = virtualKeyPosition;
    }

    public void SetEnemyType (EnemyType enemyType) {
        this.enemyType = enemyType;
    }

    public void SetSectionType(float sectionTime) {
        this.sectionTime = sectionTime;
    }

    public GestureTime GetGestureTime () {
        float finalTime = Time.time - initialTime;
        if (finalTime < sectionTime) {
            return GestureTime.TooFast;
        } else if (finalTime > 5 * sectionTime) {
            return GestureTime.TooSlow; 
        } else if (finalTime > 2 * sectionTime && finalTime < 4 * sectionTime) {
            return GestureTime.Perfect; 
        } else {
            return GestureTime.Ok;
        }
    }

    #endregion


}
