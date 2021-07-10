using UnityEngine;

public class Grid_Cell 
{
    //--- Constructor ---//
    public Grid_Cell()
    {
        AttachedItemTile = null;
    }



    //--- Public Properties ---//
    public Item_Tile AttachedItemTile { get; set; }
    public bool IsOpen { get => (AttachedItemTile != null); }
}
