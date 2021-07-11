using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public class Game_DayDesc
    {
        public Game_DayDesc()
        {
            m_sellType = Item_Type.None;
            m_deliveries = new List<Item_Type>();
        }

        public Item_Type m_sellType;
        public List<Item_Type> m_deliveries;
    }



    //--- Public Variables ---//
    public int m_numDaysUpcoming;
    [Range(0, 100)] public int m_sellDayChance;
    public Vector2Int m_deliveryCountRange;
    public TextMeshProUGUI m_txtDayCount;
    public Item_Type m_highestSpawnableItem;



    //--- Private Variables ---//
    private Queue<Game_DayDesc> m_upcomingDays;
    private Grid_ItemClearer m_gridClearer;
    private Timeline_Animate m_timelineAnimate;
    private Timeline_Manager m_timelineManager;
    private Stockpile_Manager m_stockManager;
    private int m_currentDay;



    //--- Unity Methods ---//
    private void Awake()
    {
        m_gridClearer = FindObjectOfType<Grid_ItemClearer>();
        m_timelineAnimate = FindObjectOfType<Timeline_Animate>();
        m_timelineManager = FindObjectOfType<Timeline_Manager>();
        m_stockManager = FindObjectOfType<Stockpile_Manager>();
        m_currentDay = 1;

        GenerateAllDays();
    }

    private void Start()
    {
        StartNewDay();
    }



    //--- Public Methods ---//
    public void GenerateAllDays()
    {
        m_upcomingDays = new Queue<Game_DayDesc>();

        for (int i = 0; i < m_numDaysUpcoming; i++)
            m_upcomingDays.Enqueue(GenerateNewDay());
    }

    public void StartNewDay()
    {
        // Update the UI
        m_txtDayCount.text = "DAY\n" + m_currentDay.ToString();

        // Play the animation
        m_timelineAnimate.TimelineNewDay();

        // Determine what the day holds
        var thisDay = m_upcomingDays.Dequeue();

        // If the day is a sell day, trigger a selling of the specific item type
        if (thisDay.m_sellType != Item_Type.None)
        {
            // TODO: play sounds, particles, etc
            m_gridClearer.ClearItems(thisDay.m_sellType);
        }

        // Drop in the deliveries
        StartCoroutine(m_stockManager.SpawnShipment(thisDay.m_deliveries));
    }
    
    public void EndDay()
    {
        // Generate a new day for the future
        m_upcomingDays.Enqueue(GenerateNewDay());

        // Move to the next
        m_currentDay++;

        // Start the next day
        StartNewDay();
    }

    public Game_DayDesc GenerateNewDay()
    {
        var newDayDesc = new Game_DayDesc();

        int upperItemBound = (m_highestSpawnableItem == Item_Type.All || m_highestSpawnableItem == Item_Type.Count)
                            ? (int)Item_Type.Count
                            : (int)(m_highestSpawnableItem + 1);

        // Randomly choose to be a sell day or not
        int sellDayRoll = Random.Range(0, 100);
        if (sellDayRoll < m_sellDayChance)
        {
            // If a sell day, randomly select what item to be for
            int sellItemIdx = Random.Range(0, upperItemBound);
            newDayDesc.m_sellType = (Item_Type)sellItemIdx;

            // Add an icon to the timeline that matches the type to sell
            m_timelineManager.AddNewSaleIcon(newDayDesc.m_sellType, m_numDaysUpcoming - m_upcomingDays.Count - 1);
        }

        // Randomly decide the number of deliveries
        // Need to add 1 so that the upper bound is inclusive
        int numDeliveries = Random.Range(m_deliveryCountRange.x, m_deliveryCountRange.y + 1);

        // Now, randomly decide what each of the deliveries are going to be
        for (int i = 0; i < numDeliveries; i++)
        {
            int deliveryItemIdx = Random.Range(0, upperItemBound);
            newDayDesc.m_deliveries.Add((Item_Type)deliveryItemIdx);
        }

        return newDayDesc;
    }
}
