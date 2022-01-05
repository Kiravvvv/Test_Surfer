//Набор параметров для персонажей
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]//Атрибут добавляющий здоровье
[RequireComponent(typeof(Rigidbody))]
public abstract class Game_character : Object
{
    [Header("Настройки персонажа")]
    [Space(20)]

    [Tooltip("Голова")]
    [SerializeField]
    protected Transform Head = null;

    [Tooltip("Скорость")]
    [SerializeField]
    protected float Speed_movement = 0.1f;

    protected float Speed_movement_default = 0;//Параметр скорости с которым работаем

    [Tooltip("Скорость поворота")]
    [SerializeField]
    protected float Speed_rotation = 1f;

    [Tooltip("Сила прыжка")]
    [SerializeField]
    protected float Jump_force = 100f;

    protected Rigidbody Body = null;//Физика
    protected Health Health_script = null;//Скрипт здоровья

    protected bool Control = true;//Контролирует ли игрок персонажа
    protected bool Grounded = true;//Стоит ли на замле

    protected virtual void Awake()
    {
        Health_script = GetComponent<Health>();
        Body = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();

        Speed_movement_default = Speed_movement;
    }

    /// <summary>
    /// Включить/отключить контроль персонажем
    /// </summary>
    /// <param name="_active">Активность</param>
    public virtual void Active_control(bool _active)
    {
        Control = _active;
    }

    protected virtual void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }


}
