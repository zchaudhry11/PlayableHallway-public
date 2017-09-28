using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> Keys = new List<int>();

    public bool ContainsTargetKey(int keyID)
    {
        return Keys.Contains(keyID);
    }
}