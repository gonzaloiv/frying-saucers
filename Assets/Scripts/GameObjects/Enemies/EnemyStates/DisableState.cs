using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class DisableState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            StopAllCoroutines();
            anim.Play("Disable");
            explosionPS.transform.position = transform.position;
            explosionPS.Play();
            enemyController.InvokeEnemyHitEvent();
        }

        #endregion

    }

}