using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private ParticleSystem[] arrowHitFx;
    [SerializeField] private ParticleSystem[] rockHitFx;
    [SerializeField] private ParticleSystem[] canon;
    [SerializeField] private ParticleSystem[] arbaletShoot;
    private int currentRock;
    private int currentArrow;
    private int currentCanon;
    private int currentArbaet;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayArrowHit(Vector3 pos)
    {
        arrowHitFx[currentArrow].transform.position = pos;
        arrowHitFx[currentArrow].Play();
        currentArrow++;
        if (currentArrow == arrowHitFx.Length)
            currentArrow = 0;
    }

    public void PlayRocktHit(Vector3 pos)
    {
        rockHitFx[currentRock].transform.position = pos;
        rockHitFx[currentRock].Play();
        currentRock++;
        if (currentRock == rockHitFx.Length)
            currentRock = 0;
    }
    public void PlayCanonShoot(Vector3 pos)
    {
        canon[currentCanon].transform.position = pos;
        canon[currentCanon].Play();
        currentCanon++;
        if (currentCanon == canon.Length)
            currentCanon = 0;
    }
    public void PlayArbaletShoot(Vector3 pos)
    {
        arbaletShoot[currentArbaet].transform.position = pos;
        arbaletShoot[currentArbaet].Play();
        currentArbaet++;
        if (currentArbaet == arbaletShoot.Length)
            currentArbaet = 0;
    }
}
