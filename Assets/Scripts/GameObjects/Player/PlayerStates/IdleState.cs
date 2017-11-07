using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PlayerStates {

    public class IdleState : BaseState {

        #region Fields / Properties

        private Vector2 nextPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            anim.Play("Spawn");
            StartCoroutine(IdleRoutine());
        }

        public override void Play () {
            base.Play();
            transform.position = Vector2.Lerp(transform.position, nextPosition, GameConfig.PlayerMaxSpeed / 4 * Time.deltaTime);
        }

        public override void Exit () {
            base.Exit();
            StopAllCoroutines();
        }

        public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
            SetEnemyPosition(enemyAttackEventArgs.Position);
            playerController.ToEnemyAttackState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
        }

        protected override void RemoveListeners () {
            EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator IdleRoutine () {
            nextPosition = new Vector2(new int[2]{ -2, 2 }[Random.Range(0, 2)], GameConfig.PlayerInitialYPosition);
            while (gameObject.activeInHierarchy) {
                float animationTime = Random.Range(1f, 2f);
                nextPosition = new Vector2(-nextPosition.x, nextPosition.y);
                transform.rotation = Quaternion.Euler(Vector3.zero);
                anim.Play(Random.Range(0, 2) == 0 ? "Return01" : "Return02");
                yield return new WaitForSeconds(animationTime);
            }
        }

        #endregion

    }

}