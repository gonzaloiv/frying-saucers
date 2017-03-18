using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;

public class CreditsController : MonoBehaviour, IPointerClickHandler {

  #region Fields

  [SerializeField] private GameObject title;
  [SerializeField] private List<GameObject> creditsUFOs;
  private Text[] creditsText;

  private List<GameObject> objects = new List<GameObject>();

  #endregion

  #region Mono Behaviour

  void Awake() {
    objects.Add(title);
    creditsText = GetComponentsInChildren<Text>();
    creditsUFOs.ForEach(x => objects.Add(x));
    for (int i = 0; i < creditsText.Length; i++)
      objects.Add(creditsText[i].gameObject);
  }

  void OnEnable() {
    StartCoroutine(ScrollingTextRoutine());
  }

  void Update() {
    if(objects.Last().transform.position.y >= -4)
      SceneManager.LoadScene(2);
  }

  #endregion

  #region IPointerClickHandler Behaviour

  public void OnPointerClick(PointerEventData e) {
    SceneManager.LoadScene(2);
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ScrollingTextRoutine() {
    yield return new WaitForSeconds(0.9f);
    while(gameObject.activeSelf) {
      objects.ForEach(x => x.transform.position = new Vector2(x.transform.position.x, x.transform.position.y + 0.01f));
      yield return null;
    }
  }

  #endregion


}
