using UnityEngine;

public interface IControllable 
{
    void Move(Transform target, Unit unit);
    void Death();

    bool isDeath();
}
