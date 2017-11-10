using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GestureRecognitionStates {

    public class IdleState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
        }

        public override void Exit() {
            base.Exit();
            EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
        } 

        public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
            gestureInput.SetEnemyType(enemyAttackEventArgs.EnemyType);
            gestureInput.SetInitialTime(Time.time);
            gestureRecognitionController.ToLineRecognitionState();
        }

        #endregion

    }

}