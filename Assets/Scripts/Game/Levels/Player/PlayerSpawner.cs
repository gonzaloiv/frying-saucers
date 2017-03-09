using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject shipPrefab;
  private GameObject ship;

  #endregion

  #region Public Behaviour

  public GameObject SpawnPlayer() {
    ship = Instantiate(shipPrefab, transform);
    ship.transform.position = Config.PLAYER_INITIAL_POSITION;

    return ship;
  }

  #endregion

}
