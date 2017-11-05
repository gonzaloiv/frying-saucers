using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class WaveRestartState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            player.DecreaseLives();
            anim.Play("Disable");
            explosionPS.Play();
        }

        #endregion

    }

}