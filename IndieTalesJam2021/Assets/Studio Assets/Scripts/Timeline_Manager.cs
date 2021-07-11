using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline_Manager : MonoBehaviour
{
    //Public Variables
    public GameObject saleIcon;

    //Private Variables
    [SerializeField] private RectTransform iconSpawnPos;
    [SerializeField] private GameObject timelineMask;
    [SerializeField] private List<Sprite> iconSprites;

    private Timeline_Animate timelineAnimator;

    //only used to simulate new sale colour
    //private int fruit;

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
        int fruit = Random.Range(0, 4);

        //Set which sale icon to use.
        icon.GetComponent<Image>().sprite = iconSprites[fruit];
    }
}
