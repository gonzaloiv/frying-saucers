using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingScreenController : MonoBehaviour {

    #region Fields

    private const float INIT_TIME = 1f;
    [SerializeField] private Image loadingScreenTitle;

    #endregion

    #region Public Behaviour

    void Awake () {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(LoadingRoutine());
        loadingScreenTitle.color = new Color(loadingScreenTitle.color.r, loadingScreenTitle.color.g, loadingScreenTitle.color.b, 0.0f);
    }

    void Start () {
        loadingScreenTitle.DOFade(1, INIT_TIME / 4).SetEase(Ease.InFlash);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator LoadingRoutine () {
        yield return new WaitForSeconds(INIT_TIME);
        SceneManager.LoadScene((int) GameScene.GameScene);
    }

    #endregion

}
