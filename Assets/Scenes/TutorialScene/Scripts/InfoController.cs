using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject[] infoScreenPrefabs;
    [SerializeField] private GameObject[] errorScreenPrefabs;

    private InputManager inputManager;
    private Animator anim;

    private IInfoScreenController[] infoScreens;
    private IInfoScreenController[] errorScreens;
    private int currentInfoScreen = 0;
    private int currentErrorScreen = 0;

    #endregion

    #region Mono Behaviour

    void Awake () {

        anim = GetComponent<Animator>();

        infoScreens = new IInfoScreenController[infoScreenPrefabs.Length];
        for (int i = 0; i < infoScreenPrefabs.Length; i++) {
            GameObject infoScreen = Instantiate(infoScreenPrefabs[i], transform);
            infoScreen.SetActive(false);
            infoScreens[i] = infoScreen.GetComponent<IInfoScreenController>();
            infoScreens[i].Initialize(this);
        }

        errorScreens = new IInfoScreenController[errorScreenPrefabs.Length];
        for (int i = 0; i < errorScreenPrefabs.Length; i++) {
            GameObject errorScreen = Instantiate(errorScreenPrefabs[i], transform);
            errorScreen.SetActive(false);
            errorScreens[i] = errorScreen.GetComponent<IInfoScreenController>();
            errorScreens[i].Initialize(this);
        }

    }

    void OnEnable () {
        EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
        EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
        EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
        EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StartListening<LevelEndEvent>(OnLevelEndEvent);
    }

    void OnDisable () {
        EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
        EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
        EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
        EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StopListening<LevelEndEvent>(OnLevelEndEvent);
    }

    #endregion

    #region Public Behaviour

    public void NextInfoScreen () {
        if (currentInfoScreen < infoScreens.Length) {
            if (currentInfoScreen > 0)
                infoScreens[currentInfoScreen - 1].Stop();
            infoScreens[currentInfoScreen].Play();
        } else {
            anim.Play("FadeOut");
        }
        currentInfoScreen++;
    }

    public void NextErrorScreen () {
        if (currentErrorScreen < errorScreens.Length) {
            infoScreens[currentInfoScreen].Stop();
            errorScreens[currentErrorScreen].Play();
        }
        currentErrorScreen++;
    }

    void OnEnemyAttackEvent (EnemyAttackEvent enemyAttackEvent) {
        NextInfoScreen();
        currentErrorScreen = 0;
    }

    void OnEnemyHitEvent (EnemyHitEvent EnemyHitEvent) {
        NextInfoScreen();
    }

    void OnPlayerHitEvent (PlayerHitEvent playerHitEvent) {
        NextErrorScreen();
        currentInfoScreen--;
    }

    void OnNewGameEvent (NewGameEvent newGameEvent) {
        NextInfoScreen();
    }

    void OnLevelEndEvent (LevelEndEvent levelEndEvent) {
        DataManager.SetHasBeenTutorialPlayed();
    }

    #endregion

}
