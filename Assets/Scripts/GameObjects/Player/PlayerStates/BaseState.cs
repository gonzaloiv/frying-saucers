using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class BaseState : State {

        #region Fields

        protected PlayerController playerController;
        protected Player player;
        protected ParticleSystem explosionPS;
        protected ParticleSystem laserPS;
        protected Animator anim;

        #endregion

        #region Mono Behaviour

        void Awake () {
            playerController = GetComponent<PlayerController>();
            player = playerController.Player;
            explosionPS = playerController.ExplosionPS;
            laserPS = playerController.LaserPS;
            anim = GetComponent<Animator>();
        }

        #endregion

        #region Protected Behaviour

        protected Vector2 GetEnemyPosition() {
            return playerController.EnemyPosition;
        }

        protected void SetEnemyPosition(Vector2 enemyPosition) {
            playerController.EnemyPosition = enemyPosition;
        }

        protected Quaternion QuaternionToEnemy () {
            Quaternion quaternion = Quaternion.identity;
            Vector2 moveDirection = (Vector2) transform.position - (Vector2) GetEnemyPosition(); 
            if (moveDirection != Vector2.zero) {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }
            return quaternion;
        }

        #endregion

    }

}