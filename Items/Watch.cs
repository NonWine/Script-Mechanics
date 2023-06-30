using UnityEngine;
using System.Collections;

public class Watch : Item 
{
    [SerializeField] private float timeFrozeState;
    [SerializeField] private MeshRenderer meshRenderer;
   

    protected override void Action()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 400);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Dire"))
            {
                Unit unit = colliders[i].GetComponent<Unit>();
                if (unit.GetType() == Mob.Mimic)
                {
                    unit.Death();
                }
                else
                {
                    unit.GetComponent<ControllStateUnit>().FrozeState(timeFrozeState);
                }
            }
        }
        Spawn();
    }

    protected override IEnumerator Respawn()
    {
          UnitManager.Instance.haveApteka = false;
        UIManager.Instance.ActiveColdPanel();
        GetComponent<SphereCollider>().enabled = false;
        ps.gameObject.SetActive(false);
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(timeFrozeState);
        UIManager.Instance.DeActiveColdPanel();
        Destroy(gameObject);
        yield break;

    }
}
