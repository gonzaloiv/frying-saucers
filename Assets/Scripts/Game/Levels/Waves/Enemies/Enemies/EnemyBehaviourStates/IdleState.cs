using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

  public class IdleState : BaseState {

    #region Public Behaviour

    public override void Enter() {
      transform.rotation = Quaternion.identity;
    }

    public override void Play() {
      if(!hit)
        transform.position = Vector2.Lerp(transform.position, enemyController.Enemy.Position, Config.ENEMY_MAX_SPEED * Time.timeScale);
    }

    #endregion

  }

}