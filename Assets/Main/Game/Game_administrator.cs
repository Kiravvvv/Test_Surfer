//Скрипт общего управления во время игры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_administrator : Singleton<Game_administrator>
{
    /// <summary>
    /// Делегат для событий относящихся к контролю игрока 
    /// </summary>
    /// <param name="_bool">Параметр активации</param>
    public delegate void Player_control_delegate(bool _bool);
    /// <summary>
    /// Переменная Делегата для событий относящихся к контролю игрока 
    /// </summary>
    public Player_control_delegate PCD;

    /// <summary>
    /// Делегат для событий относящихся к замедлению
    /// </summary>
    /// <param name="_active">Параметр активации</param>
    public delegate void Time_dilation_delegate(bool _active);
    /// <summary>
    /// Переменная Делегата для событий относящихся к замедлению
    /// </summary>
    public Time_dilation_delegate Time_dilation_d;

    [Tooltip("Фактор замедления")]
    [SerializeField]
    protected float Time_dilation_factor = 0.01f;

    bool Time_dilation_bool = false;//Режим времени (замедленный ли)

    /// <summary>
    /// Нормализовать время
    /// </summary>
    void Normal_time()
    {
        if (!Time_dilation_bool)
            Time_dilation_active(false);
    }

    /// <summary>
    ///  Сменить активность контроля игрока над персонажем
    /// </summary>
    /// <param name="_active">Включение или выключение</param>
    public void Player_control_active(bool _active)
    {
        PCD?.Invoke(_active);
    }


    /// <summary>
    /// Закончить игру
    /// </summary>
    /// <param name="_win">Победа?</param>
    public void End_game(bool _win)
    {
        Normal_time();
        Invoke("Normal_time", 0.6f);

        if(_win)
            Invoke("Delay_End_game_win", 2.2f);
        else
            Invoke("Delay_End_game_lose", 1.6f);

    }

    /// <summary>
    /// Закончить игру в виде победы (функция для задержки)
    /// </summary>
    void Delay_End_game_win()
    {
        Interface_administrator.Instance.Game_win();
    }

    /// <summary>
    /// Закончить игру в виде поражения (функция для задержки)
    /// </summary>
    void Delay_End_game_lose()
    {
        Interface_administrator.Instance.Game_lose();
    }

    /// <summary>
    /// Включить замедление времени
    /// </summary>
    /// <param name="_active">Включить замедление?</param>
    public void Time_dilation_active(bool _active)
    {
        if (_active != Time_dilation_bool)
        {
            Time_dilation_d?.Invoke(_active);
            Time_dilation_bool = _active;
        }
    }

    /// <summary>
    /// Узнать замедлено время сейчас ли
    /// </summary>
    public bool Find_out_time_dilation_bool
    {
        get
        {
            return Time_dilation_bool;
        }
    }

    /// <summary>
    /// Узнать фактор замедления
    /// </summary>
    public float Find_out_Time_dilation_factor
    {
        get
        {
            return Time_dilation_factor;
        }
    }


}
