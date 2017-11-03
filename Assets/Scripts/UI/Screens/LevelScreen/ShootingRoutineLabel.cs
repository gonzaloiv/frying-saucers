using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRoutineLabel : MonoBehaviour {

    #region Fields

    private static string[] signs = new string[] { "•••", "3", "2", "1", "0" , "x" };
    private Text label;

    private IEnumerator shootingRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        label = GetComponent<Text>();
        label.text = signs[0];
    }

    void OnEnable () {
        EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
        EnemyController.EnemyHitEvent += OnEnemyHitEvent;
    }

    void OnDisable () {
        EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
        EnemyController.EnemyHitEvent -= OnEnemyHitEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        shootingRoutine = ShootingRoutine(enemyAttackEventArgs.SectionTime);
        StartCoroutine(shootingRoutine);
    }

    public void OnEnemyHitEvent () {
        if (shootingRoutine != null)
            StopCoroutine(shootingRoutine);
        label.text = signs[0];
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShootingRoutine (float sectionTime) {
        for (int i = 0; i < signs.Length; i++) { 
            label.text = signs[i];
            yield return new WaitForSeconds(sectionTime);
        }
        label.text = signs[0]; 
    }

    #endregion
	
}
