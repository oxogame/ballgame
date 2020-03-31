using DG.Tweening;
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
            float flyTime = 2.2f;
            RefBall.DOMove(KickPosition + Vector3.up * 2, flyTime * .5f).SetEase(Ease.InOutElastic);
                //.OnComplete(()=>
                //RefBall.DOMove(KickPosition, flyTime * .5f).SetEase(Ease.InFlash)
            //);
        }
    }

    
}

