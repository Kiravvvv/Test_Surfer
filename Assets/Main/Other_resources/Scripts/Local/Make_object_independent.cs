//Скрипт, что бы сделать объект самостоятельным (отделить от родителя)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_object_independent : MonoBehaviour
{

    [Header("Настройки для самостоятельности объекта")]
    [Space(20)]

    [Tooltip("Физика тела")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Основной коллайдер")]
    [SerializeField]
    Collider Main_collider = null;

    private void Start()
    {
        Body.isKinematic = true;
        Main_collider.enabled = false;
    }

    /// <summary>
    /// Освобождает объект от рабства! (Делает самостоятельным и отделяет от родительского объекта)
    /// </summary>
    public void Activation()
    {
        Body.isKinematic = false;
        Main_collider.enabled = true;

        transform.SetParent(null);
    }

}
