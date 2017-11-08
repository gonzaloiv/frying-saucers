using System.Collections.Generic;
using UnityEngine;

public class Board {

    #region Fields

    private static Vector2 boardSize;

    #endregion

    #region Public Behaviour

    public Board (Camera gameCamera) {
        Board.boardSize = gameCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2.5f;
    }

    public static Vector2 GetRandomOutOfBoardPosition () {
        if (new bool[] { true, false }[Random.Range(0, 2)]) {
            return new Vector2(new float[]{ -boardSize.x, boardSize.x }[Random.Range(0, 1)], Random.Range(0, boardSize.y * 2));  
        } else {
            return new Vector2(Random.Range(-boardSize.x, boardSize.x), boardSize.y);
        }
    }

    public static Vector2 EmptyEnemyShotPosition () {
        Vector2 position = EnemyShotPosition();
        while (Physics2D.OverlapCircle(position, 1))
            position = EnemyShotPosition();
        return position;
    }
 
    public static Vector2[] EnemyGrid (int enemyAmount) {
        Vector2[] grid = new Vector2[enemyAmount];
        float positionXSize = boardSize.x / (grid.Length + 2);
        for (int x = 0; x < enemyAmount; x++)
            grid[x] = new Vector2(-boardSize.x / 2 + ((positionXSize) * (x + 1)) + positionXSize / 2, GameConfig.EnemyInitialYPosition);
        return grid;
    }

    #endregion

    #region Private Behaviour

    private static Vector2 EnemyShotPosition () {
        return new Vector2(Random.Range(-boardSize.x / 4, boardSize.x / 4), 1);
    }

    #endregion
}