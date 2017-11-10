using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class EnemyAttackState : BaseState {

        #region Fields / Properties

        private Vector2 enemyPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            enemyPosition = GetEnemyPosition();
        }

        public override void Play () {
            base.Play();
            transform.rotation = QuaternionToEnemy();
            transform.position = Vector2.Lerp(transform.position, new Vector2(enemyPosition.x, transform.position.y), GameConfig.PlayerMaxSpeed / 4 * Time.deltaTime);
        }

        public override void Exit () {
            base.Exit();
        }

        public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
            playerController.ToRespawnState();
        }

        public void OnRightGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
            StartCoroutine(AttackRoutine());
            player.IncreaseCombo();
            player.IncreaseScore(gestureInputEventArgs.Time);
        }

        public void OnWrongGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
            player.ResetCombo();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EnemyController.EnemyShotEvent += OnEnemyShotEvent;
            GestureRecognitionController.RightGestureInputEvent += OnRightGestureInputEvent;
            GestureRecognitionController.WrongGestureInputEvent += OnWrongGestureInputEvent;
        }

        protected override void RemoveListeners () {
            EnemyController.EnemyShotEvent -= OnEnemyShotEvent;
            GestureRecognitionController.RightGestureInputEvent -= OnRightGestureInputEvent;
            GestureRecognitionController.WrongGestureInputEvent -= OnWrongGestureInputEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator AttackRoutine () {
            laserPS.Play();
            playerController.InvokePlayerShotEvent();
            yield return new WaitForSeconds(0.3f);
            playerController.ToIdleState();
        }

        #endregion

    }

}