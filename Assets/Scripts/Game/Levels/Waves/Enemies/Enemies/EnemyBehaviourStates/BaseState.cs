using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

  public class BaseState : State {

    #region Fields

    protected Animator animator;
    protected GameObject player;
    protected ParticleSystem laser;
    protected EnemyController enemyController;
    protected bool hit;
    protected float routineTime;

    private EnemyBehaviour enemyBehaviour;

    #endregion

    #region Mono Behaviour

    void Awake() {
      enemyBehaviour = GetComponent<EnemyBehaviour>();
      animator = GetComponent<Animator>();
      player = enemyBehaviour.Player;
      laser = enemyBehaviour.Laser;
      enemyController = GetComponent<EnemyController>();
      hit = enemyBehaviour.Hit;
      routineTime = enemyBehaviour.RoutineTime;
    }

    #endregion

  }

}