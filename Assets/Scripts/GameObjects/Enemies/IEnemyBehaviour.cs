using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehaviour {

    void Init (GameObject player);
    void Play (float routineTime);
    void Stop ();

}
