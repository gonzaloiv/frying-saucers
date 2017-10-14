using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenController : MonoBehaviour {

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
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
        PlayerController.PlayerHitEvent += OnPlayerHitEvent;
        GameController.NewGameEvent += OnNewGameEvent;
        LevelController.LevelEndEvent += OnLevelEndEvent;
        GameController.NewGameEvent += OnNewGameEvent;
    }

    void OnDisable () {
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
        PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
        GameController.NewGameEvent -= OnNewGameEvent;
        LevelController.LevelEndEvent -= OnLevelEndEvent;
        GameController.NewGameEvent -= OnNewGameEvent;
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

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        NextInfoScreen();
        currentErrorScreen = 0;
    }

    public void OnEnemyHitEvent () {
        NextInfoScreen();
    }

    public void OnPlayerHitEvent () {
        NextErrorScreen();
        currentInfoScreen--;
    }

    public void OnNewGameEvent () {
        NextInfoScreen();
    }

    public void OnLevelEndEvent () {
        DataManager.SetHasBeenTutorialPlayed();
    }

    #endregion

}
