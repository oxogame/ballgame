using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Animation Data List", order = 1)]
public class RV_AnimDataList : ScriptableObject
{
    public List<AnimationVo> list = new List<AnimationVo>();
}