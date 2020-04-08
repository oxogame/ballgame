using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Level Data List", order = 2)]
public class LevelData : SerializedScriptableObject
{
    public Dictionary<string, LevelSpecs> levelDatas = new Dictionary<string, LevelSpecs>();
}
