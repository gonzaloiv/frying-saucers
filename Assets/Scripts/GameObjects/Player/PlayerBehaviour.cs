using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour {

    #region Fields

    public const float MAX_SPEED = 10f;
    public static Vector2[] PLAYER_POSITIONS;
 
    private Collider2D col;

    private Vector2 nextPosition;
    private Vector2 enemyPosition;
    private bool rightGesture;

    #endregion

    #region Mono Behaviour

    void Awake () {
        col = GetComponent<Collider2D>();
    }

    void Update () {
        transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED * Time.deltaTime);
    }

    void OnEnable () {
        nextPosition = new Vector2(0, GameConfig.PlayerInitialYPosition);
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent += OnEnemyShotEvent;
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInput;
    }

    void OnDisable () {
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyBehaviour.EnemyShotEvent -= OnEnemyShotEvent;
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInput;
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        enemyPosition = enemyAttackEventArgs.Position;
        rightGesture = false;
    }

    public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        if (rightGesture)
            StartCoroutine(EvasionRoutine());
    }

    public void OnRightGestureInput (RightGestureInputEventArgs ightGestureInputEventArgs) {
        nextPosition.x = enemyPosition.x;
        rightGesture = true;
    }

    #endregion

    #region Private Behaivour

    private IEnumerator EvasionRoutine () {
        nextPosition.x = nextPosition.x + new float[]{ -2.5f, 2.5f }[Random.Range(0, 2)];
        col.enabled = false;
        yield return new WaitForSeconds(1);
        col.enabled = true;
        nextPosition.x = 0;
        rightGesture = false;
    }

    #endregion

}
  