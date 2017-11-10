using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialScreenController : MonoBehaviour {

    #region Fields

    private const float ANIMATION_TIME = 0.3f;

    [SerializeField] private Text infoPanelLabel;

    [SerializeField] private InfoScreen introInfoScreen;
    [SerializeField] private InfoScreen endingInfoScreen;
    [SerializeField] private InfoScreen errorInfoScreen;

    private InfoScreen currentInfoScreen;

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        InputManager.TapInputEvent += OnTapInputEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
        GestureRecognitionController.WrongGestureInputEvent += OnWrongGestureInputEvent;
    }

    void OnDisable () {
        InputManager.TapInputEvent -= OnTapInputEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
        GestureRecognitionController.WrongGestureInputEvent -= OnWrongGestureInputEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init(){
        introInfoScreen.ResetInfoScreenIndex();
        currentInfoScreen = introInfoScreen;
        SetInfoPanelLabelText();
    }

    public void OnTapInputEvent () {
        if(!currentInfoScreen.IsLastInfoScreenText)
            currentInfoScreen.IncreaseInfoScreenIndex();
        SetInfoPanelLabelText();
    }

    public void OnEnemyHitEvent () {
        currentInfoScreen = endingInfoScreen;
        currentInfoScreen.ResetInfoScreenIndex();
        SetInfoPanelLabelText();
    }

    public void OnWrongGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
        currentInfoScreen = errorInfoScreen;
        currentInfoScreen.ResetInfoScreenIndex();
        SetInfoPanelLabelText();
    }

    #endregion

    #region Private Behaviour

    private void SetInfoPanelLabelText() {
        string infoPanelLabelText = string.Empty;
        currentInfoScreen.CurrentInfoScreenText.ForEach(line => infoPanelLabelText += line + "\n");
        infoPanelLabel.text = infoPanelLabelText;
        Time.timeScale = currentInfoScreen.IsLastInfoScreenText ? GameConfig.GameTimeScale : 0;
        DOTween.Sequence().Append(infoPanelLabel.DOFade(0, ANIMATION_TIME)).Append(infoPanelLabel.DOFade(1, ANIMATION_TIME));
    }

    #endregion

}
