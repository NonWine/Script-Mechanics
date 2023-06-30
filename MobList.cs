using System.Collections.Generic;
using UnityEngine;

public static class MobList
{
    private static List<GameObject> mob = new List<GameObject>();

    public static void AddDinoInList(GameObject weapon) => mob.Add(weapon);
    public static int GetAmount() { return mob.Count; }
    public static GameObject GetDino(int index) { return mob[index]; }
}
