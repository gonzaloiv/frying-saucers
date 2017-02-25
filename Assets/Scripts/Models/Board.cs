using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Models {

  public struct Board {

    #region Fields

    public static Vector2 BOARD_SIZE { get { return boardSize; } }
    private static Vector2 boardSize = new Vector2(2, 4);

    public static Vector2 SCREEN_SIZE { get { return screenSize; } set { screenSize = value; } }
    private static Vector2 screenSize;

    #endregion

    #region Public Behaviour

    public Board(Vector2 screenSize) {
      Board.screenSize = screenSize;
    }

    #endregion

  }

}