using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroyBehaviour : MonoBehaviour {

    #region Fields / Properties

    private ParticleSystem ps;

    #endregion

    #region Mono Behaviour

    void Start () {
        ps = GetComponent<ParticleSystem>();
    }

    void Update () {
        if (ps && !ps.IsAlive())
            Destroy(gameObject);
    }

    #endregion

}