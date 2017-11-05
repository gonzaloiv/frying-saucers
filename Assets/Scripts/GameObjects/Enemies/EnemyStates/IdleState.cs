using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class IdleState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            transform.rotation = Quaternion.identity;
        }

        public override void Play () {
            base.Play();
            transform.position = Vector2.Lerp(transform.position, enemyController.Enemy.Position, GameConfig.EnemyMaxSpeed / 2 * Time.deltaTime);
        }

        #endregion

    }

}