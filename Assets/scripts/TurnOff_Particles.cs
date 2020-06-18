using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff_Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    void Update()
    {
        if (!particles.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
