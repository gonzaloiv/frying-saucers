using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject shipPrefab;
  private GameObject ship;

  #endregion

  #region Public Behaviour

  public GameObject SpawnPlayer(Vector2 position, List<GameObject> levelObjects) {
    ship = Instantiate(shipPrefab, transform);
    ship.transform.position = position;
    ship.GetComponent<PlayerBehaviour>().Initialize(levelObjects);

    return ship;
  }

  #endregion

}
