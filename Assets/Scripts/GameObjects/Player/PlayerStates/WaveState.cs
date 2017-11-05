using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class WaveState : BaseState {

        #region Fields / Properties

        private Vector2 nextPosition;
        private Vector2 enemyPosition;

        private bool rightGesture;
        private bool shot;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            nextPosition = new Vector2(0, GameConfig.PlayerInitialYPosition);
            shot = false;
        }

        public override void Play() {
            base.Play();
            transform.position = Vector2.Lerp(transform.position, nextPosition, GameConfig.PlayerMaxSpeed * Time.deltaTime);
        }

        void OnParticleCollision (GameObject particle) {
            if (!shot && particle.layer == (int) CollisionLayer.Enemy) {
                playerController.ToWaveRestartState();
                shot = true;
            }
        }

        public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
            enemyPosition = enemyAttackEventArgs.Position;
            rightGesture = false;
        }

        public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
            if (rightGesture)
                StartCoroutine(EvasionRoutine());
        }

        public void OnRightGestureInput (GestureInputEventArgs gestureInputEventArgs) {
            nextPosition.x = enemyPosition.x;
            playerWeaponController.Shoot(enemyPosition);
            rightGesture = true;
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
            EnemyController.EnemyShotEvent += OnEnemyShotEvent;
            GestureManager.RightGestureInputEvent += OnRightGestureInput;
        }

        protected override void RemoveListeners () {
            EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
            EnemyController.EnemyShotEvent -= OnEnemyShotEvent;
            GestureManager.RightGestureInputEvent -= OnRightGestureInput;
        }

        #endregion

        #region Private Behaivour

        private IEnumerator EvasionRoutine () {
            nextPosition.x = nextPosition.x + new float[]{ -2.5f, 2.5f }[Random.Range(0, 2)];
            col.enabled = false;
            yield return new WaitForSeconds(1);
            col.enabled = true;
            nextPosition.x = 0;
            rightGesture = false;
        }

        #endregion

    }

}