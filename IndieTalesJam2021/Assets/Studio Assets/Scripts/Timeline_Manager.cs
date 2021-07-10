using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline_Manager : MonoBehaviour
{
    //Public Variables
    public GameObject saleIcon;

    //Private Variables
    [SerializeField] private RectTransform iconSpawnPos;
    [SerializeField] private GameObject timelineMask;

    private Timeline_Animate timelineAnimator;

    // Start is called before the first frame update
    void Start()
    {
        timelineAnimator = FindObjectOfType<Timeline_Animate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            AddNewSaleIcon();
    }

    public void AddNewSaleIcon()
    {
        //Create new icon
        GameObject saleIconObject = Instantiate(saleIcon);
        
        //set proper colour of sale icon
        SetSaleIcon(saleIconObject);

        //Properly set transform and parent of new icon
        saleIconObject.transform.SetParent(timelineMask.transform);
        saleIconObject.transform.localPosition = iconSpawnPos.localPosition;

        //Add new icon to list of animated icons
        timelineAnimator.AddSaleIconToList(saleIconObject);
    }

    private void SetSaleIcon(GameObject icon)
    {
        int fruit = 0;

        //Set which sale icon to use.
    }
}
