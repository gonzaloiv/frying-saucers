using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Based on TheLiquidFire's: https://theliquidfire.wordpress.com/2015/07/06/object-pooling/
public class GameObjectArrayPool : IPool {

  #region Fields

  private GameObject[] prefabs;
  private GameObject pool;

  private List<KeyValuePair<int, GameObject>> objects = new List<KeyValuePair<int, GameObject>>();
  private int currentPrefabIndex = 0;

  #endregion

  #region Contructors

	public GameObjectArrayPool(string poolName, GameObject[] prefabs, int initialObjectAmount, Transform parent) {
    this.prefabs = prefabs;
    pool = new GameObject(poolName);
    pool.transform.parent = parent;
    Prepopulate(initialObjectAmount);
  }

  #endregion

  #region Public Behaviour

  public GameObject PopObject() {
    foreach (KeyValuePair<int, GameObject> pair in objects) {
      if (!pair.Value.activeInHierarchy)
        return pair.Value;
    }
    return PushObject();
  }

  public GameObject PopObject(int prefabIndex) {
    List<GameObject> prefabObjects = objects.Where(x => x.Key == prefabIndex).Select(x => x.Value).ToList();
    foreach (GameObject obj in prefabObjects) {
      if (!obj.activeInHierarchy)
        return obj;
    }
    return PushObject(prefabIndex, prefabs[prefabIndex]);
  }

  public GameObject PopRandomObject() {
    return PopObject(UnityEngine.Random.Range(0, prefabs.Length));
  }

  public GameObject PushObject() {
    currentPrefabIndex = currentPrefabIndex < prefabs.Length - 1 ? currentPrefabIndex + 1: 0;
    return PushObject(currentPrefabIndex, prefabs[currentPrefabIndex]);
  }

  public GameObject PushObject(int index, GameObject prefab) {
    GameObject obj = MonoBehaviour.Instantiate(prefab, pool.transform);
    obj.SetActive(false);
    objects.Add(new KeyValuePair<int, GameObject>(index, obj));

    return obj;
  }

  #endregion

  #region Private Behaviour

  private void Prepopulate(int objectAmount) {
    int x = 0, y = currentPrefabIndex;
    while (x < objectAmount) {
      PushObject(y, prefabs[y]);
      y = y < prefabs.Length - 1 ? y += 1 : 0 ;
      x++;
    }
    currentPrefabIndex = y - 1;
  }

  #endregion

}