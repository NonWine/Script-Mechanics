using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    //ALSO NEED PHYSICS MATERIAL
    [SerializeField] private float _force;
    [SerializeField] private Rigidbody rd;
    [SerializeField] private float timeAlive;
    private bool trig;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)));
        Invoke("Disable", timeAlive);
    }

    public void Force(Vector3 vec)
    {
        rd.velocity = vec * _force;
        trig = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && !trig)
        {
            trig = true;
        }
    }


}
