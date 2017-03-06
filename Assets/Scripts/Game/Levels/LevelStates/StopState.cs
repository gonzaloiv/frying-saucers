using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class StopState : BaseState {

    #region State Behaviour

    public override void Enter() {  
      StopAllCoroutines();
      currentLevelObjects.ForEach(x => x.SetActive(false));
    }

    #endregion

  }

}