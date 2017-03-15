using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Models;
using LevelStates;

public class LevelSpawner : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject hudPrefab;

  [SerializeField] private GameObject backgroundPrefab;
  private GameObject background;

  #endregion

  #region Mono Behaviour

  void Awake() {
    background = Instantiate(backgroundPrefab, transform);
  }

  void OnEnable() {
    background.SetActive(true);
  }

  void OnDisable() {
    background.SetActive(false);
  }

  #endregion

  #region Public Behaviour

  public HUDController HUDController() {
    return Instantiate(hudPrefab, transform).GetComponent<HUDController>();
  }

  public WaveController WaveController() {
    return Instantiate(wavePrefab, transform).GetComponent<WaveController>();
  }

  public GameObject Player() {
    PlayerSpawner playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    return playerSpawner.SpawnPlayer();
  }

  #endregion
	
}
