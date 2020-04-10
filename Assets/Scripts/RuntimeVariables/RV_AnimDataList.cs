using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Animation Data List", order = 1)]
public class RV_AnimDataList : SerializedScriptableObject
{
    //[ShowInInspector]
    //[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<string,TransitionVo> tranList = new Dictionary<string, TransitionVo>();
    public Dictionary<string, AnimationMoveVo> moveFigureList = new Dictionary<string, AnimationMoveVo>();
}


