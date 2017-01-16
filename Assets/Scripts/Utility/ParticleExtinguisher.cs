using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class ParticleExtinguisher : MonoBehaviour {

    private ParticleSystemDestroyer destroyer;

    void Start()
    {
        destroyer = GetComponent<ParticleSystemDestroyer>();
    }

    public void StopParticles()
    {
        if (destroyer != null)
            destroyer.Stop();
    }

    public void OnEnable()
    {
        GameEventManager.GameReset += StopParticles;
    }

    public void OnDisable()
    {
        GameEventManager.GameReset -= StopParticles;
    }
}
