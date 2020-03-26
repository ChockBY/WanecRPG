using UnityEngine;
using System.Collections;
namespace ARPGDemo.Backpack
{
    public class GridPanelUI : MonoBehaviour
    {
        public Transform[] Grids;

        public Transform[] StateGrids;
        public Transform GetEmptyGrid()
        {
            for (int i = 0; i < Grids.Length; i++)
            {
                if (Grids[i].childCount == 0)
                    return Grids[i];
            }
            return null;
        }
        public Transform GetStateEmptyGrid()
        {
            for (int i = 0; i < StateGrids.Length; i++)
            {
                if (StateGrids[i].childCount == 0)
                    return StateGrids[i];
            }
            return null;
        }
    }
    
}

