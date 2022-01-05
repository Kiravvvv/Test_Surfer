//Скрипт для включения и отключения Тряпичной куклы(Ragdoll)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_active : MonoBehaviour
{
    [Space(20)]
    [Header("Основное")]

    [Tooltip("Аниматор персонажа")]
    [SerializeField]
    Animator Anim = null;

    [Tooltip("Коллайдеры основные (не куклы)")]
    [SerializeField]
    Collider[] Collider_base_array = new Collider[0];

    [Tooltip("Физика всех частей основные (не куклы)")]
    [SerializeField]
    Rigidbody[] Rigidbody_base_array = new Rigidbody[0];



    [Space(20)]
    [Header("Кукла")]

    [Tooltip("Коллайдеры куклы")]
    [SerializeField]
    Collider[] Collider_array = new Collider[0];

    [Tooltip("Физика всех частей куклы")]
    [SerializeField]
    Rigidbody[] Rigidbody_array = new Rigidbody[0];

    [Tooltip("Откидывание от игрока")]
    [SerializeField]
    float Force_push = 1000;

    [Tooltip("Активирует тряпичную куклу при старте сцены")]
    [SerializeField]
    bool Start_ragdoll = false;

    bool Active_bool = false;//Активирован ли уже режим куклы

    private void Start()
    {
        for (int x = 0; x < Rigidbody_array.Length; x++)
        {
            Rigidbody_array[x].isKinematic = true;
            Collider_array[x].enabled = false;

        }

        if (Start_ragdoll)
        {
            Active_change(true);
        }

    }

    /// <summary>
    /// Изменить активность куклы
    /// </summary>
    /// <param name="_active">Активность</param>
    public void Active_change(bool _active)
    {
        if (!Active_bool && _active || Active_bool && !_active)
        {
            Active_bool = true;

            Anim.enabled = !_active;

            foreach (Collider collider in Collider_base_array)
            {
                collider.enabled = !_active;
            }

            foreach (Rigidbody rigidbody in Rigidbody_base_array)
            {
                rigidbody.useGravity = !_active;

                rigidbody.isKinematic = _active;
            }

            foreach (Collider collider in Collider_array)
            {
                collider.enabled = _active;
            }

            foreach (Rigidbody rigidbody in Rigidbody_array)
            {
                rigidbody.useGravity = _active;

                rigidbody.isKinematic = !_active;
            }

        }


    }

    /// <summary>
    /// Изменить активность куклы с придаванием ей импульса от цели
    /// </summary>
    /// <param name="_active">Активность</param>
    /// <param name="_player">Цель от которой будет отлетать</param>
    public void Active_change(bool _active, Transform _player)
    {
        Active_change(_active);

        if (_active)
        {

            for (int x = 0; x < Rigidbody_array.Length; x++)
                {
                    if (_player != null)
                    {
                        Vector3 direction = transform.position - _player.position;
                        Rigidbody_array[x].AddForce((direction + new Vector3(0, 7, 0)) * Force_push);
                    }

                }
            
        }

    }

}
