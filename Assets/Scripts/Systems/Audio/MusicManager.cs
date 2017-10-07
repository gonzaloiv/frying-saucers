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
        LevelController.NewLevelEvent += OnNewLevelEvent;
        LevelScreenController.GameOverEvent += OnGameOverEvent;
        GameController.CreditsEvent += OnCreditsEvent;
        GameController.NewGameEvent += OnNewGameEvent;
    }

    void OnDisable () {
        LevelController.NewLevelEvent -= OnNewLevelEvent;
        LevelScreenController.GameOverEvent -= OnGameOverEvent;
        GameController.CreditsEvent -= OnCreditsEvent;
        GameController.NewGameEvent -= OnNewGameEvent;
    }

    #endregion

    #region Event Behaviour

    void OnNewGameEvent (NewGameEventArgs newGameEventArgs) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.Menu];
        audioSource.Play();
    }

    void OnNewLevelEvent (NewLevelEventArgs newLevelEventArgs) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.Level];
        audioSource.Play();
    }

    void OnGameOverEvent (GameOverEventArgs gameOverEventArgs) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.GameOver];
        audioSource.Play();
    }

    void OnCreditsEvent (CreditsEventArgs creditsEventArgs) {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) Music.GameOver];
        audioSource.Play();
    }

    #endregion

}
