using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Models {

  public struct Enemy {

    #region Fields

    public EnemyType EnemyType { get { return enemyType; } }
    private EnemyType enemyType;

    public Vector2 Position { get { return position; } }
    private Vector2 position;

    #endregion

    #region Public Behaviour

    public Enemy(EnemyType enemyType, Vector2 position) {
      this.enemyType = enemyType;
      this.position = position;
    }

    #endregion
	
  }

}