using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

  public class IdleState : BaseState {

    #region Public Behaviour

    public override void Enter() {
      animator.Play("Idle");
      transform.rotation = Quaternion.identity;
    }

    public override void Play() {
      transform.position = Vector2.Lerp(transform.position, enemyController.Enemy.Position, Config.ENEMY_MAX_SPEED * Time.timeScale);
    }

    #endregion 

  }

}