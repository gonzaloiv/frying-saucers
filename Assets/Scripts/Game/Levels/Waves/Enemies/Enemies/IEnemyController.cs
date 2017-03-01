using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public interface IEnemyController {

  void Initialize(Enemy enemy);
  void Disable();
  void StopBehaviour();
	
}
