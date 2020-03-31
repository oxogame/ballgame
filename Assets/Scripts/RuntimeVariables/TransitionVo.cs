﻿using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class TransitionVo
    {
        public string Name;

        public float TransitionTime;

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

