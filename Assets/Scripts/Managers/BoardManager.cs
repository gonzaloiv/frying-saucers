using System.Collections.Generic;
using Models;
using UnityEngine;

public class BoardManager {

  #region Fields

  public static Vector2 BOARD_SIZE { get { return boardSize; } }
  private static Vector2 boardSize;

  public static Vector2 SCREEN_SIZE { get { return screenSize; } set { screenSize = value; } }
  private static Vector2 screenSize;

  public static Vector2[,] ENEMY_GRID { get { return enemyGrid; } set { enemyGrid = value; } }
  private static Vector2[,] enemyGrid;

  public static Vector2 ENEMY_SHOT_POSITION { get { return enemyShotPosition; } }
  private static Vector2 enemyShotPosition = new Vector2(0, 1);

  private Camera camera;

  #endregion

  #region Public Behaviour

  public BoardManager(Camera camera, Vector2 screenSize) {
    this.camera = camera;
    BoardManager.screenSize = screenSize;
    BoardManager.boardSize = camera.ScreenToWorldPoint(screenSize);
    EnemyGrid(new Vector2(BoardManager.boardSize.x * 4, BoardManager.boardSize.y * 4));
  }

  public static Vector2 GetEnemyGridPosition(int index, int wavePosition) {
    return BoardManager.ENEMY_GRID[index, wavePosition];
  }

  public static Vector2 GetRandomOutOfBoardPosition() {
    bool axis = new bool[] {true, false}[Random.Range(0, 2)];
    if(axis == true)
      return new Vector2(new float[]{-boardSize.x, boardSize.x}[Random.Range(0, 1)], Random.Range(0, boardSize.y * 2));  
    else
      return new Vector2(Random.Range(-boardSize.x, boardSize.x), boardSize.y);
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
