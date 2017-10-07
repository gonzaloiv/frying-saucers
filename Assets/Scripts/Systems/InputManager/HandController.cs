using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    #region Fields

    // 0 => index, 1 => right, 2 => wrong
    [SerializeField] private List<GameObject> handPrefabs;
    private List<GameObject> hands;

    [SerializeField] private GameObject tracePrefab;
    private GameObjectPool tracePool;

    private GameObject currentHand = null;
    private Vector2 currentPosition;
    private GameObject currentParticle = null;

    #endregion

    #region Mono Behaviour

    void Awake () {
        hands = new List<GameObject>();
        handPrefabs.ForEach(x => {
            GameObject hand = Instantiate(x, transform);
            hand.SetActive(false);
            hands.Add(hand);
        });
        tracePool = new GameObjectPool("TracePool", tracePrefab, 10, transform);
    }

    void OnEnable () {
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;
    }

    void OnDisable () {
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;
    }

    #endregion

    #region Public Behaviour

    public void SetHand (int index, Vector2 position) {

        currentPosition = position;

        if (currentHand != hands[index]) {
            if (currentHand != null)
                currentHand.SetActive(false);
            currentHand = hands[index];
        }

        currentHand.SetActive(true);
        currentHand.transform.position = position;
        // Adapts hand position to mouse position
        currentHand.transform.Translate(new Vector2(0f, -0.2f));

        SetHandParticle(position);

    }

    public void RemoveHand () {
        if (currentHand != null)
            currentHand.SetActive(false); 
    }

    public void OnRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        StartCoroutine(GestureRoutine(1));    
    }

    public void OnWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputEventArgs) {
        StartCoroutine(GestureRoutine(2));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator GestureRoutine (int index) {
        SetHand(index, currentPosition);
        yield return TimeManager.WaitForRealTime(index);
        RemoveHand();
    }

    public void SetHandParticle (Vector2 position) {
        if (currentParticle != null)
            currentParticle.SetActive(false);
        GameObject trace = tracePool.PopObject();
        trace.transform.position = position;
        trace.SetActive(true);
        trace.GetComponent<ParticleSystem>().Play();
    }

    #endregion

}
