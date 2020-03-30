using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class AnimationVo
    {
        public string Name;

        public float Duration;

        public float KickFrame;

        public Vector3 KickPosition;

        [Title("Temporary Datas")]
        public Transform RefBall;
        [Button]
        public void GetBallPosition()
        {
            KickPosition = RefBall.position;
        }
    }

    
}

