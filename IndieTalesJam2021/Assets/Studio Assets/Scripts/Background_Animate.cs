using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Background_Animate : MonoBehaviour
{
    public float tweenDist, tweenDuration;

    [SerializeField] private GameObject clouds;
    private Vector3 cloudsStartPos;

    // Start is called before the first frame update
    void Start()
    {
        cloudsStartPos = clouds.transform.localPosition;

        AnimateClouds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateClouds()
    {
        //Tween Position of timeline
        clouds.transform.DOLocalMoveX(tweenDist, tweenDuration, false)
            .OnComplete(ResetPosition).OnComplete(AnimateClouds);

    }

    private void ResetPosition()
    {
        clouds.transform.localPosition = cloudsStartPos;
    }
}
