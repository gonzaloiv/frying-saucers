using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UFOGridController : MonoBehaviour {

  #region Fields

  [SerializeField] private List<GameObject> ufos;
  private Animator anim;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
  }

  #endregion

  #region Public Behaviour

  public void Play() {
    StartCoroutine(DisablingRoutine());
  }

  public void Stop() {
    anim.Play("FadeOut");
  }

  #endregion

  #region Private Behaviour

  private IEnumerator DisablingRoutine() {
    while(gameObject.activeSelf) {
      yield return new WaitForSeconds(.3f);
      List<GameObject> activeUFOs = ufos.Where(x => x.activeSelf).ToList();
      if(activeUFOs.Count() > 0)
        activeUFOs[Random.Range(0, activeUFOs.Count())].GetComponent<EnemyController>().DisableRoutine();
      else
        StartCoroutine(RestartGridRoutine());
    }
  }

  private IEnumerator RestartGridRoutine() {
    for (int i = 0; i < ufos.Count(); i++) { 
      ufos[i].SetActive(true);
      yield return new WaitForSeconds(.1f); 
    }
  }

  #endregion
	
}
