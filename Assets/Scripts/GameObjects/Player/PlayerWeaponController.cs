using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject laserPrefab;

    private ParticleSystem laser;
    private Vector2 enemyPosition = Vector2.zero;

    #endregion

    #region Events

    public delegate void PlayerShotEventHandler ();
    public static event PlayerShotEventHandler PlayerShotEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
    }

    void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, QuaternionToClick(), 1);
    }

    void OnEnable () {
        enemyPosition = Vector2.zero; 
    }

    #endregion

    #region Public Behaviour

    public void Shoot (Vector2 enemyPosition) {
        laser.Play();
        this.enemyPosition = enemyPosition;
    }

    #endregion

    #region Private Behaviour

    private Quaternion QuaternionToClick () { 

        Quaternion quaternion = Quaternion.identity;
        Vector2 moveDirection = (Vector2) transform.position - enemyPosition;

        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }

        return quaternion;
    }

    #endregion

}
