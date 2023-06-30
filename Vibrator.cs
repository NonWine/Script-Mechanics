using UnityEngine;
using System.Collections;
public  class Vibrator: MonoBehaviour
{
    private static bool Shake;
    [SerializeField] private float _Duration = 1f;
    [SerializeField] private AnimationCurve animationCurve;
    private void Update()
    {
      
        if (Shake)
        {
            Shake = false;
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {

        Quaternion startRot;
        Vector3 startPosition = transform.localPosition;
        startRot = transform.rotation;
        float ElapsedTime = 0f;
        while (ElapsedTime < _Duration)
        {
            ElapsedTime += Time.deltaTime;
            float strenght = animationCurve.Evaluate(ElapsedTime / _Duration);
            transform.localPosition = startPosition + Random.insideUnitSphere * strenght;

            yield return null;
        }
        transform.localPosition = startPosition;
    }
    public static void CreateShake()
    {
        Shake = true;
    }
}
