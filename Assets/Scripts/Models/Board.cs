using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Board {

  #region Fields

  public Vector2 BoardSize { get { return boardSize; } set { boardSize = value; } }
  private Vector2 boardSize;

  #endregion

  #region Public Behaviour	

  public Board (Vector2 boardSize) {
    this.boardSize = boardSize;
	}
	
  #endregion

}
