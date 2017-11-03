using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class BaseState : State {

        #region Fields

        protected EnemyController enemyController;
        protected Animator animator;
        protected GameObject player;
        protected ParticleSystem laser;
        protected bool hit;
        protected float routineTime;

        #endregion

        #region Mono Behaviour

        void Awake () {
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();
            player = enemyController.Player;
            laser = enemyController.Laser;
            enemyController = GetComponent<EnemyController>();
            hit = enemyController.Hit;
            routineTime = enemyController.RoutineTime;
        }

        #endregion

    }

}