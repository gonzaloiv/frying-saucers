using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/Level", order = 1)]
public class LevelData : ScriptableObject {

    public List<WaveData> WavesData { get { return wavesData; } }
    [SerializeField] private List<WaveData> wavesData;

}