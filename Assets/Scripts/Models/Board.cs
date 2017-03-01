using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Models {

  public struct Board {

    #region Fields

    public static Vector2 BOARD_SIZE { get { return boardSize; } }
    private static Vector2 boardSize;

    public static Vector2 SCREEN_SIZE { get { return screenSize; } set { screenSize = value; } }
    private static Vector2 screenSize;

    public static Vector2[,] ENEMY_GRID { get { return enemyGrid; } set { enemyGrid = value; } }
    private static Vector2[,] enemyGrid;

    private Camera camera;

    #endregion

    #region Public Behaviour

    public Board(Camera camera, Vector2 screenSize) {
      this.camera = camera;
      Board.screenSize = screenSize;
      Board.boardSize = camera.ScreenToWorldPoint(screenSize);
      EnemyGrid(new Vector2(Board.boardSize.x * 4, Board.boardSize.y * 4));
    }

    public static Vector2 GetEnemyGridPosition(int index, int wavePosition) {
      return Board.ENEMY_GRID[index, wavePosition];
    }

    #endregion

    #region Private Behaviour

    private void EnemyGrid(Vector2 boardSize) {

      enemyGrid = new Vector2[Config.ENEMY_GRID_ROW_SIZE, Config.ENEMY_GRID_COL_SIZE];
      float positionXSize = boardSize.x / (Config.ENEMY_GRID_ROW_SIZE + 2);
      float positionYSize = boardSize.y / (Config.ENEMY_GRID_COL_SIZE + 2);

      for (int x = 0; x < Config.ENEMY_GRID_ROW_SIZE; x++) {
        for (int y = 0; y < Config.ENEMY_GRID_COL_SIZE; y++) {
            enemyGrid[x, y] = new Vector2(
              -boardSize.x / 2 + ((positionXSize) * (x + 1)) + positionXSize / 2,
              positionYSize / 1.5f * y
            );
          }
        }

    }

    #endregion

  }

}