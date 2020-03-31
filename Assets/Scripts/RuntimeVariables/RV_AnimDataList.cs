using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Animation Data List", order = 1)]
public class RV_AnimDataList : SerializedScriptableObject
{
    public List<AnimationVo> animlist = new List<AnimationVo>();

    //[ShowInInspector]
    //[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<string,TransitionVo> tranlist = new Dictionary<string, TransitionVo>();
}


