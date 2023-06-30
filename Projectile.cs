using UnityEngine;
using NTC.Global.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType myType;
    private int dmg;

    private void Update()
    {
        if(myType == ProjectileType.Granate)
        {
            transform.Rotate(Random.insideUnitSphere * 80f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Dire"))
        {
            Debug.Log(dmg);
            other.transform.GetComponent<Unit>().GetDamage(dmg,dmg);
            if (myType == ProjectileType.Explosive || myType == ProjectileType.Granate)
                GetComponent<Explosive>().Explose();
            NightPool.Despawn(gameObject);
        }
    }

    public void SetDamage(int value) => dmg = value;

    public bool isFrost()
    {
        if (myType == ProjectileType.Frost)
            return true;
        else
            return false;
    }
}
enum ProjectileType
{
    Default, Fire,Explosive,Frost,Granate
}
