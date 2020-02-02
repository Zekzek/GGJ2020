using System.Linq;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    [HideInInspector]
    public InventoryGridCell[] cells;

    // Start is called before the first frame update
    void Awake()
    {
        cells = GetComponentsInChildren<InventoryGridCell>()
            .OrderBy(go => -go.transform.position.y)
            .ThenBy(go => go.transform.position.x)
            .ToArray();
    }

    public void SetUpWithInventory(BasicInventory bi)
    {
        for (int n=0; n < cells.Length; n++)
        {
            int x = n % BasicInventory.ROW_WIDTH;
            int y = n / BasicInventory.ROW_WIDTH;

            if (bi.IsInBounds(x,y))
            {
                cells[n].SetGridCell(bi, x, y);
            }
            else
            {
                Debug.LogError("InventoryGrid out of bounds of real inventory. Index=" + n);
                cells[n].ClearCell();
            }
        }
    }

    public void ClearInventory()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
