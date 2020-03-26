using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class playCanvasAnim : MonoBehaviour {

    private DOTweenAnimation tweenAnimation;
    private bool isClick = false;
    private AudioSource source;
	void Start () {
        tweenAnimation = this.GetComponent<DOTweenAnimation>();
        source = GetComponent<AudioSource>();
	}
	
    public void PlayAnim()
    {
        if(isClick==false)
        {
            tweenAnimation.DOPlayForward();
            isClick = true;
            if(source)
            source.Play();
        }
    }
    public void CancelPlay()
    {
        if (isClick)
        {
            tweenAnimation.DOPlayBackwards();
            isClick = false;
        }
    }
}
