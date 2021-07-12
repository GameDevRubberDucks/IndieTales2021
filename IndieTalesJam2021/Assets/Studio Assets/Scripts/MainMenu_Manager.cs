using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenu_Manager : MonoBehaviour
{
    public GameObject credits;

    [SerializeField] private float creditsStartPos;
    [SerializeField] private float creditsEndPos;
    [SerializeField] private float tweenDur;

    [SerializeField] private bool creditsShown;

    //private Util_SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        creditsShown = false;

        creditsStartPos = credits.GetComponent<RectTransform>().transform.localPosition.x;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        //sceneLoader.LoadSceneByName("Main");
    }

    public void ShowCredits()
    {
        if (!creditsShown)
        {

            credits.GetComponent<RectTransform>().DOLocalMoveX(creditsEndPos, tweenDur);
            creditsShown = true;
        }           
        else
        {
            credits.GetComponent<RectTransform>().DOLocalMoveX(creditsStartPos, tweenDur);
            creditsShown = false;
        }

    }
}
