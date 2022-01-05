//Здоровье 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    /// <summary>
    /// Делегат для событий относящихся к тому кто атакует 
    /// </summary>
    /// <param name="_killer">Атакующий</param>
    public delegate void Killer_attack(Game_character _killer);
    /// <summary>
    /// Переменная Делегата для событий относящихся к тому кто атакует 
    /// </summary>
    public Killer_attack Killer_attack_delegate;

    [Space(20)]
    [Header("Основное")]

    [Tooltip("Является ли игроком")]
    [SerializeField]
    private bool Player_bool = false;

    [Tooltip("Количество жизней")]
    [SerializeField]
    private int Health_active = 10;

    private int Health_default = 0;//Параметр для манипуляции с жизнями

    [Tooltip("Аниматор")]
    [SerializeField]
    protected Animator Anim = null;

    [Tooltip("Не умирает")]
    [SerializeField]
    private bool No_death_bool = false;

    [Tooltip("Уничтожается сразу когда заканчивается здоровье")]
    [SerializeField]
    private bool Death_destroy_bool = false;

    private Game_character Killer = null;//Кто убил




    [Space(20)]
    [Header("Дополнительно")]


    [Tooltip("Система частиц после смерти(не обязательно)")]
    [SerializeField]
    private ParticleSystem Death_PS = null;

    [Tooltip("Скрипт тряпичной куклы (не обязательно)")]
    [SerializeField]
    Ragdoll_active Ragdoll_script = null;

    [Tooltip("Скрипт мигания при получение урона (не обязательно)")]
    [SerializeField]
    Blinking_effect Blinking_effect_script = null;



    private bool Alive_bool = true;//Является ли живым

    protected Transform My_transform = null;//Трансформ объекта 

    private void Start()
    {
        My_transform = transform;

        if (GetComponent<Animator>())
            Anim = GetComponent<Animator>();

        Health_default = Health_active;
    }

    /// <summary>
    /// Изменение здоровья
    /// </summary>
    /// <param name="_change">На какое значение изменить</param>
    private void Change_health(int _change)
    {

        Health_active += _change;

        if (Health_active <= 0)
        {
            Health_active = 0;

            if (!No_death_bool)
            {
                if (Death_PS)
                    Instantiate(Death_PS, My_transform.position, Quaternion.identity);

                Death();
            }
        }
        else if (Health_active > Health_default)
        {
            Health_active = Health_default;
        }

        if(Player_bool)
        {
            Interface_administrator.Instance.Health_info((float)Health_active / (float)Health_default);
        }

    }

    /// <summary>
    /// Смерть/разрушение объекта
    /// </summary>
    private void Death()
    {
        Alive_bool = false;



        if (Anim)
            Anim.Play("Death");

        if (Ragdoll_script)
        {
            Ragdoll_script.Active_change(true);
        }
        else if (Death_destroy_bool)
        {
            Destroy(gameObject);
        }
           
    }

    /// <summary>
    /// Получение урона с указанием кто атаковал
    /// </summary>
    /// <param name="_damage">Значение урона</param>
    /// <param name="_killer">Кто атаковал</param>
    public void Damage_add(int _damage, Game_character _killer)
    {
        Killer = _killer;

        if (Alive_bool)
        {
            Change_health(-_damage);

            Killer_attack_delegate?.Invoke(_killer);

            if (Player_bool)
            {
                Interface_administrator.Instance.Damage_anim_effect();
            }

            if (Anim)
            {
                Anim.Play("Harm");
            }

            if(Blinking_effect_script)
            Blinking_effect_script.Activation();

        }
    }

    /// <summary>
    /// Узнать жив ли персонаж
    /// </summary>
    public bool Find_out_Alive
    {
        get
        {
            return Alive_bool;
        }
    }
}
