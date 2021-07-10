using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Timeline_Animate : MonoBehaviour
{
    //Public Variables
    public float tweenDist;
    public float tweenDuration;

    //Private Variables
    [SerializeField] private RectTransform timeline;
    [SerializeField] private List<GameObject> existingSaleIcons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TimelineNewDay();
    }

    public void AddSaleIconToList(GameObject saleIcon)
    {
        existingSaleIcons.Add(saleIcon);
    }

    public void TimelineNewDay()
    {
        //Tween Position of timeline
        timeline.DOLocalMoveX(tweenDist, tweenDuration, false)
            .OnComplete(ResetTimeline);

        //Loop through all the sale icons and tween positions.
        for (int i = 0; i < existingSaleIcons.Count; i++)
        {
            existingSaleIcons[i].GetComponent<RectTransform>().DOLocalMoveX(existingSaleIcons[i].GetComponent<RectTransform>().localPosition.x + tweenDist, tweenDuration, false);      
        }

        ClearOldIcons();
    }

    private void ClearOldIcons()
    {
        //Loop through all the sale icons and check x positions
        for (int i = 0; i < existingSaleIcons.Count; i++)
        {
            if (existingSaleIcons[i].GetComponent<RectTransform>().localPosition.x > 400)
            {
                Destroy(existingSaleIcons[i]);
                existingSaleIcons.Remove(existingSaleIcons[i]);
            }
        }
    }

    private void ResetTimeline()
    {
        //Reset timeline position back to start
        timeline.transform.localPosition = new Vector3(0, -30);
    }
}
