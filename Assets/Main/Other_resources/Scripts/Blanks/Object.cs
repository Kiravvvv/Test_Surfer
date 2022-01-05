//Обычный объект
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour
{
    [Header("Настройки объекта")]
    [Space(20)]

    [Tooltip("Имя объекта")]
    public string Name = "Имя объекта";

    [Tooltip("Аниматор")]
    [SerializeField]
    protected Animator Anim = null;

    [Tooltip("Скрипт для управления звуками")]
    [SerializeField]
    protected Sound_control Sound_control_ = null;

    protected Transform My_transform = null;//Трансформ объекта 

    protected virtual void Start()
    {
        My_transform = transform;
    }

}
