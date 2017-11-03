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
        GestureManager.RightGestureInputEvent += OnRightGestureInput;
        EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
        enemyPosition = Vector2.zero; 
    }

    void OnDisable () {
        GestureManager.RightGestureInputEvent -= OnRightGestureInput;
        EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnRightGestureInput (GestureInputEventArgs gestureInputEventArgs) {
        laser.Play();
        PlayerShotEvent.Invoke();
    }

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        enemyPosition = enemyAttackEventArgs.Position;
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
