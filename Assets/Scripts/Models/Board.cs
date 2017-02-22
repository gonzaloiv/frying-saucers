using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Board {

  #region Fields

  public static Vector2 BoardSize { get { return boardSize; } }
  private static Vector2 boardSize = new Vector2(5, 5);

  public static Vector2 ScreenSize { get { return screenSize; } set { screenSize = value; } }
  private static Vector2 screenSize;

  #endregion

  #region Public Behaviour	

  public Board (Vector2 screenSize) {
    Board.screenSize = screenSize;
	}
	
  #endregion

}
