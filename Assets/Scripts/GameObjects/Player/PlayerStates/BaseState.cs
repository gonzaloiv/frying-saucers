using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class BaseState : State {

        #region Fields

        protected PlayerController playerController;
        protected PlayerWeaponController playerWeaponController;
        protected Player player;
        protected ParticleSystem explosionPS;
        protected Animator anim;
        protected Collider col;

        #endregion

        #region Mono Behaviour

        void Awake () {
            playerController = GetComponent<PlayerController>();
            playerWeaponController = GetComponent<PlayerWeaponController>();
            player = playerController.Player;
            explosionPS = playerController.ExplosionPS;
            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
        }

        #endregion

    }

}