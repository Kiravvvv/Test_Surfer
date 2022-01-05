using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface_administrator : Singleton<Interface_administrator>
{
    [Header("Показатели игрока и его интерфейс")]

    [Tooltip("Показатель сдоровья игрока")]
    [SerializeField]
    Image Health_image = null;

    [Tooltip("Аниматор получения урона")]
    [SerializeField]
    Animator Damage_anim = null;

    [Tooltip("Игровое меню")]
    [SerializeField]
    GameObject Game_menu_canvas = null;

    [Tooltip("Включено ли игровое меню")]
    public bool Game_menu_bool = false;




    [Header("Прочее")]
    [Space(20)]
    [Tooltip("Меню выиграной игры")]
    [SerializeField]
    GameObject Canvas_game_wins = null;

    [Tooltip("Меню проигранной игры")]
    [SerializeField]
    GameObject Canvas_game_lose = null;

    public void Damage_anim_effect()//Получить урон (анимация мигания)
    {
        Damage_anim.Play("Damage");
    }

    public void Health_info(float _value)//Изменить показатели жизней
    {
        Health_image.fillAmount = _value;
    }




    public void Game_menu_active(bool _active)//Включение/Выключение игрового меню
    {
        Game_menu_bool = _active;
        Game_menu_canvas.SetActive(_active);
    }

    public void Game_win()//Конец игры (победил)
    {
        Canvas_game_wins.SetActive(true);
        Game_administrator.Instance.Player_control_active(false);
        Game_Player.Cursor_player(true);
    }

    public void Game_lose()//Конец игры (проиграл)
    {
        Canvas_game_lose.SetActive(true);
        Game_administrator.Instance.Player_control_active(false);
        Game_Player.Cursor_player(true);
    }

}
