using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicManager : MonoBehaviour {

    #region Fields

    [SerializeField] private List<AudioSource> audioSources;

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StartListening<NewLevelEvent>(OnNewLevelEvent);
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
    }

    void OnDisable () {
        EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StopListening<NewLevelEvent>(OnNewLevelEvent);
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    }

    #endregion

    #region Event Behaviour

    void OnNewGameEvent (NewGameEvent newGameEvent) {
        audioSources.ForEach(audioSource => audioSource.Stop());
        audioSources[0].Play();
    }

    void OnNewLevelEvent (NewLevelEvent newLevelEvent) {
        audioSources.ForEach(audioSource => audioSource.Stop());
        audioSources[1].Play();
    }

    void OnGameOverEvent (GameOverEvent gameOverEvent) {
        audioSources.ForEach(audioSource => audioSource.Stop());
        audioSources[2].Play();
    }

    #endregion

}
