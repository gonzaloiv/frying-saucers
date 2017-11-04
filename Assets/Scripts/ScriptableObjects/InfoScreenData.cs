using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfoScreenData", menuName = "ScriptableObject/InfoScreen", order = 1)]
public class InfoScreenData : ScriptableObject {

    [SerializeField] public List<InfoScreenText> Texts;

}
