using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController {

  void Initialize(EnemyType enemyType);
  void Disable();
	
}
