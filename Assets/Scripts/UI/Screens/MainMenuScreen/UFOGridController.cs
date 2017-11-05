using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UFOGridController : MonoBehaviour {

    #region Fields

    [SerializeField] private float disablingSpeed = 0.3f;
    [SerializeField] private List<GameObject> ufos;

    #endregion

    #region Public Behaviour

    void OnEnable () {
        StartCoroutine(DisablingRoutine());
    }

    void OnDisable () {
        StopAllCoroutines();
    }

    #endregion

    #region Private Behaviour

    private IEnumerator DisablingRoutine () {
        ufos.ForEach(ufo => ufo.SetActive(true));
        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(disablingSpeed);
            List<GameObject> activeUFOs = ufos.Where(x => x.activeSelf).ToList();
            if (activeUFOs.Count() > 0) {
                activeUFOs[Random.Range(0, activeUFOs.Count())].GetComponent<EnemyController>().ToDisableState();
            } else {
                StartCoroutine(RestartGridRoutine());
            }
        }
    }

    private IEnumerator RestartGridRoutine () {
        for (int i = 0; i < ufos.Count(); i++) { 
            ufos[i].SetActive(true);
            yield return new WaitForSeconds(.1f); 
        }
    }

    #endregion
	
}
