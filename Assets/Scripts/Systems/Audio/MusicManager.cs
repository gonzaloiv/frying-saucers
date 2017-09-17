using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

  #region Fields
 
  [SerializeField] private AudioSource newGame;
  [SerializeField] private AudioSource gameOver;

  #endregion

  #region Mono Behaviour

  void OnEnable () {
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable () {
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    if(gameOver.isPlaying)
      gameOver.Stop();
    newGame.Play();
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    if(newGame.isPlaying)
      newGame.Stop();
    gameOver.Play();
  }

  #endregion

}
