using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class UnscaledTimeParticleSystem : MonoBehaviour {

    #region Fields / Properties

    private ParticleSystem ps;

    #endregion

    #region Mono Behaviour

    void Awake () {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    void Update () {
        ps.Simulate(Time.unscaledDeltaTime, true, false);
    }

    #endregion

}