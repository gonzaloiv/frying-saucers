using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject laserPrefab;
    private ParticleSystem laser;

    [SerializeField] private GameObject powerWavePrefab;
    private ParticleSystemController powerWaveController;

    private Vector2 enemyPosition;

    #endregion

    #region Events

    public delegate void PlayerShotEventHandler (PlayerShotEventArgs playerShotEventArgs);
    public static event PlayerShotEventHandler PlayerShotEvent;

    #endregion

    #region Mono Behaviour

    void Awake () {
        laser = Instantiate(laserPrefab, transform).GetComponent<ParticleSystem>();
        powerWaveController = Instantiate(powerWavePrefab, transform.parent).GetComponent<ParticleSystemController>();
    }

    void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, QuaternionToClick(), 1);
    }

    void OnEnable () {
        enemyPosition = Vector2.zero;
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInput;
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;

    }

    void OnDisable () {
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInput;
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnRightGestureInput (RightGestureInputEventArgs rightGestureInputEventArgs) {
        laser.Play();
        if(PlayerShotEvent != null)
            PlayerShotEvent.Invoke(new PlayerShotEventArgs());
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
