using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject shipPrefab;
  public static GameObject ship;

  #endregion
  
  #region Public Behaviour

  public GameObject SpawnPlayer(Vector2 position) {
    ship = Instantiate(shipPrefab, transform);
    ship.transform.position = position;

    return ship;
  }

  #endregion

}
