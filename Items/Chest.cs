using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform krishka;
    private bool open;
    private float timer;
    protected override void Action()
    {
        if(UpgradeManger.Instance.currentActiveDeckaCout > 0 || UpgradeManger.Instance.currentDeckaCount > 0)
        open = true;


    }

    private void Update()
    {
        if (open)
        {
            timer += Time.deltaTime;
            krishka.transform.rotation = Quaternion.Lerp(krishka.transform.rotation, Quaternion.Euler(0f, 0f, 0f), timer * 0.5f);
            if(krishka.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
            {
                open = false;
                timer = 0f;
                UpgradeManger.Instance.OpenChestPanel();
                Spawn();
            }
        }
    }

    protected override IEnumerator Respawn()
    {
        UnitManager.Instance.haveChest = false;
        Destroy(gameObject);
        yield break;

    }
}
