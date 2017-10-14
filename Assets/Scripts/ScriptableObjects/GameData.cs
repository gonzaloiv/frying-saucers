using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObject/Game", order = 1)]
public class GameData : ScriptableObject {

    public List<LevelData> Levels { get { return levels; } }
    [SerializeField] private List<LevelData> levels;

}
