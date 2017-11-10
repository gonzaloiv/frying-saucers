using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {

    public class BaseState : State {

        #region Fields

        protected EnemyController enemyController;
        protected GameObject player;
        protected Enemy enemy  { get { return enemyController.Enemy; } }
        protected ParticleSystem laserPS;
        protected ParticleSystem explosionPS;
        protected Animator anim;

        #endregion

        #region Mono Behaviour

        void Awake () {
            enemyController = GetComponent<EnemyController>();
            player = enemyController.Player;
            laserPS = enemyController.LaserPS;
            explosionPS = enemyController.ExplosionPS;
            anim = GetComponent<Animator>();
        }

        #endregion

    }

}