using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class BaseState : State {

        #region Fields

        protected EnemyController enemyController;
        protected GameObject player;
        protected Enemy enemy;
        protected ParticleSystem laserPS;
        protected ParticleSystem explosionPS;
        protected Animator anim;

        #endregion

        #region Mono Behaviour

        void Awake () {
            enemyController = GetComponent<EnemyController>();
            player = enemyController.Player;
            enemy = enemyController.Enemy;
            laserPS = enemyController.LaserPS;
            explosionPS = enemyController.ExplosionPS;
            anim = GetComponent<Animator>();
        }

        #endregion

    }

}