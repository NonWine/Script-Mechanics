using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private ExplosiveType myType;
    [SerializeField] private float _radius;
    [SerializeField] private int damageExplose;
    [SerializeField] private float frozeTime;
    public void Explose()
    {
        
        if (myType == ExplosiveType.Explose)
            ParticlePool.Instance.PlayExplossion(transform.position);
        else if (myType == ExplosiveType.Frozen)
        {
            ParticlePool.Instance.PlayFrozenExplose(transform.position);
        }
           
        Collider[] enemies =  Physics.OverlapSphere(transform.position, _radius);
        foreach (var item in enemies)
        {
            if (item.CompareTag("Dire"))
            {
                Unit enemy = item.GetComponent<Unit>();
                 enemy.GetDamage(damageExplose);
                if (myType == ExplosiveType.Frozen)
                    enemy.FrozeState(frozeTime);
            }
        } 
    }
    
    public void IncreaseFrostTime(int value)
    {
        frozeTime += value;
    }
}

public enum ExplosiveType {Explose, Frozen }