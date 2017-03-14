using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public interface IGameData {

  Level[] Levels { get; } 
  void Initialize();

}
