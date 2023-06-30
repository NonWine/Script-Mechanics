using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualArrows : MonoBehaviour
{
    [SerializeField] private List<Transform> arrows = new List<Transform>(5);
    [SerializeField] private MobStats stats;
    [SerializeField] private int percentToUnlockFirstArrow;
    private float startHealth;
    private int index;
    private float increasingPercent;
    private List<Transform> startArrow;
    private void Start()
    {
      
        startHealth = stats.GetHealth();
    }

    public void VisualArrow(int dmg)
    {
        if (arrows.Count <= 0)
            return;
        float percent = ((startHealth - (float)dmg) / startHealth) * 100;
        increasingPercent += (100 - (int)percent);
        if (increasingPercent >= percentToUnlockFirstArrow)
        {
            index = Random.Range(0, arrows.Count);
            arrows[index].gameObject.SetActive(true);
            increasingPercent -= percentToUnlockFirstArrow;
            arrows.RemoveAt(index);
            VisualArrow(0);
            VisualArrow(0);
            VisualArrow(0);
        }
           
    }

    public void ResetArrows()
    {
        increasingPercent = 0;
        foreach (var item in arrows)
        {
            item.gameObject.SetActive(false);
        }
    }
}
