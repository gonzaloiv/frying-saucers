using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderboardBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Fields

  private InputManager inputManager;
  private Animator anim;
  private bool active = false;
  private Text[] scores = new Text[3];
  private Text[] dates = new Text[3];

  #endregion

  #region Mono Behaviour

  void Awake() {

    anim = GetComponent<Animator>();
    inputManager = GameObject.FindObjectOfType<InputManager>();

    // TODO: ver como hacer esto decentemente
    scores[0] = GetComponentsInChildren<Text>()[1];
    scores[1] = GetComponentsInChildren<Text>()[3];
    scores[2] = GetComponentsInChildren<Text>()[5];
    dates[0] = GetComponentsInChildren<Text>()[2];
    dates[1] = GetComponentsInChildren<Text>()[4];
    dates[2] = GetComponentsInChildren<Text>()[6];

  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    if(active)
      anim.Play("FadeOut");
  }

  #endregion

  #region Public Behaviour

  public void Play() {
    anim.Play("FadeIn");
    SetScores();
    gameObject.SetActive(true);
    inputManager.enabled = false;
    active = true;
  }

  public void Disable() {
    gameObject.SetActive(false);  
    inputManager.enabled = true;
    active = false;
    EventManager.TriggerEvent(new NewGameEvent());     
  }

  #endregion

  #region Private Behaviour

  private void SetScores() {
    for(int i = 0; i < scores.Length; i++) {
      scores[i].text = DataManager.Leaderboard.Scores[i].ToString();
      dates[i].text = DataManager.Leaderboard.Dates[i].ToString("yyyy/MM/dd");
    }
  }

  #endregion
	
}