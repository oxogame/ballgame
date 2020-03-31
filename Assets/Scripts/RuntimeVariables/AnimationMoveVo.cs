using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts
{
    [Serializable]
    public class AnimationMoveVo // this one presents all move figures in the game.
    {
        public Vector3 BallPosition;
        [Title("Temporary Datas")]
        public Transform RefBall;
        [Button]
        public void GetBallPosition()
        {
            BallPosition = RefBall.position;
        }
    }
}
