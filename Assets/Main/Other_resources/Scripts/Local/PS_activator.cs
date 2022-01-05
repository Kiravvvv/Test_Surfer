//Включает воспроизведение системы частиц
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_activator : MonoBehaviour
{

    [Tooltip("Система частиц")]
    [SerializeField]
    ParticleSystem PS = null;

    /// <summary>
    /// Включает воспроизведение системы частиц
    /// </summary>
    public void Play_PS()
    {
        PS.Play();
    }

}
