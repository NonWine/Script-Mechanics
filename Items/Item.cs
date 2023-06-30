using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected float cdRespawn;
    [SerializeField] protected ParticleSystem ps;
    private void Start()
    {
        ParticlePool.Instance.PlaySpawnItemFx(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Action();
            
       
        }
    }

    protected virtual void Action()
    {
        
    }

    public void Spawn()
    {
       StartCoroutine(Respawn());
    }


    protected virtual IEnumerator Respawn()
    {
        throw new System.NotImplementedException();
    }
}
public enum ItemType { Bomb, Watch, Magnet,Apteka, Chest }