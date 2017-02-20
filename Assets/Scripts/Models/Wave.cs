using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Wave {

  public List<Enemy> Enemies { get { return enemies; } }
  private List<Enemy> enemies;

  public Wave(List<Enemy> enemies) {
    this.enemies = enemies;
  }
  
}
