using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private bool particleWhenWalk = true;
    [SerializeField] private ParticleSystem dustParticle;

    public void CreateParticle()
    {
        if (particleWhenWalk)
        {
            dustParticle.Stop();
            dustParticle.Play();
        }
    }
}
