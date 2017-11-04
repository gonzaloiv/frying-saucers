using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Fields

    [SerializeField] GameObject jetPrefab;
    [SerializeField] GameObject explosionPrefab;

    private GameObject jet;
    private ParticleSystem explosion;
    private Animator anim;
    private Collider2D col;

    private Player player;
    private PlayerWeaponController playerWeapon;

    private Vector2 nextPosition;
    private Vector2 enemyPosition;
    private bool rightGesture;
    private bool shot;

    #endregion

    #region Mono Behaviour

    void Awake () {
        anim = GetComponent<Animator>();
        jet = Instantiate(jetPrefab, transform);
        explosion = Instantiate(explosionPrefab, transform).GetComponent<ParticleSystem>();
        playerWeapon = GetComponent<PlayerWeaponController>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable () {
        nextPosition = new Vector2(0, GameConfig.PlayerInitialYPosition);
        anim.Play("Spawn");
        AddListeners();
    }

    void Update () {
        transform.position = Vector2.Lerp(transform.position, nextPosition, GameConfig.PlayerMaxSpeed * Time.deltaTime);
    }

    void OnDisable () {
        jet.SetActive(false);
        RemoveListeners();
    }

    void OnParticleCollision (GameObject particle) {
        if (!shot && particle.layer == (int) CollisionLayer.Enemy) {
            player.DecreaseLives();
            anim.Play("Disable");
            explosion.Play();
            shot = true;
        }
    }

    void AddListeners () {
        EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyController.EnemyShotEvent += OnEnemyShotEvent;
        GestureManager.RightGestureInputEvent += OnRightGestureInput;
    }

    void RemoveListeners () {
        EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyController.EnemyShotEvent -= OnEnemyShotEvent;
        GestureManager.RightGestureInputEvent -= OnRightGestureInput;
    }

    #endregion

    #region Public Behaviour

    public void Init (Player player) {
        this.player = player;
    }

    public void Reset () {
        playerWeapon.enabled = true;
        jet.SetActive(true);
        shot = false;
    }

    public void Disable () {
        gameObject.SetActive(false);
    }

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        enemyPosition = enemyAttackEventArgs.Position;
        rightGesture = false;
    }

    public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
        if (rightGesture)
            StartCoroutine(EvasionRoutine());
    }

    public void OnRightGestureInput (GestureInputEventArgs gestureInputEventArgs) {
        nextPosition.x = enemyPosition.x;
        rightGesture = true;
        playerWeapon.Shoot(enemyPosition);
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