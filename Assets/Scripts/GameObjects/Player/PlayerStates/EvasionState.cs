using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class EvasionState : BaseState {

        #region Private Behaviour

        private Vector2 nextPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            StartCoroutine(EvasionRoutine());
            nextPosition = new Vector2(transform.position.x + new float[]{ -2.5f, 2.5f }[Random.Range(0, 2)], transform.position.y);
        }

        public override void Play () {
            base.Play();
            transform.rotation = QuaternionToEnemy();
            transform.position = Vector2.Lerp(transform.position, nextPosition, GameConfig.PlayerMaxSpeed * Time.deltaTime);
        }

        public override void Exit () {
            base.Exit();
        }

        #endregion

        #region Private Behaivour

        private IEnumerator EvasionRoutine () {
            yield return new WaitForSeconds(1);
            playerController.ToIdleState();
        }

        #endregion

    }

}