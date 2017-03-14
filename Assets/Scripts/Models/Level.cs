using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models {

  public struct Level {

    public List<Wave> Waves { get { return waves; } }
    private List<Wave> waves;
   
    public Level(List<Wave> waves) {
      this.waves = waves;
    }

  }

}