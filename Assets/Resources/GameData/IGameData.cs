using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public interface IGameData {

  Player Player { get; } 
  Level[] Levels { get; } 
  void InitializePlayer();
  void InitializeLevels();

}
