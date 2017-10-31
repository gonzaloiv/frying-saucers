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
        Player.PlayerHitEvent += OnPlayerHitEvent;
        CreditsScreenController.CreditsEvent += OnCreditsEvent;
        GameController.NewGameEvent += OnNewGameEvent;
    }

    void OnDisable () {
        LevelController.NewLevelEvent -= OnNewLevelEvent;
        Player.PlayerHitEvent -= OnPlayerHitEvent;
        CreditsScreenController.CreditsEvent -= OnCreditsEvent;
        GameController.NewGameEvent -= OnNewGameEvent;
    }

    #endregion

    #region Event Behaviour

    void OnNewGameEvent () {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) MusicTrack.Menu];
        audioSource.Play();
    }

    void OnNewLevelEvent () {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) MusicTrack.Level];
        audioSource.Play();
    }

    void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        if(!playerHitEventArgs.IsDead)
            return;
        audioSource.Stop();
        audioSource.clip = audioClips[(int) MusicTrack.GameOver];
        audioSource.Play();
    }

    void OnCreditsEvent () {
        audioSource.Stop();
        audioSource.clip = audioClips[(int) MusicTrack.GameOver];
        audioSource.Play();
    }

    #endregion

}
