using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicManager : MonoBehaviour {

    #region Fields

    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource audioSource;

    #endregion

    #region Mono Behaviour

    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable () {
        EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StartListening<NewLevelEvent>(OnNewLevelEvent);
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StartListening<CreditsEvent>(OnCreditsEvent);
    }

    void OnDisable () {
        EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
        EventManager.StopListening<NewLevelEvent>(OnNewLevelEvent);
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StopListening<CreditsEvent>(OnCreditsEvent);
    }

    #endregion

    #region Event Behaviour

    void OnNewGameEvent (NewGameEvent newGameEvent) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.Menu];
        audioSource.Play();
    }

    void OnNewLevelEvent (NewLevelEvent newLevelEvent) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.Level];
        audioSource.Play();
    }

    void OnGameOverEvent (GameOverEvent gameOverEvent) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.GameOver];
        audioSource.Play();
    }

    void OnCreditsEvent (CreditsEvent creditsEvent) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.GameOver];
        audioSource.Play();
    }

    #endregion

}
