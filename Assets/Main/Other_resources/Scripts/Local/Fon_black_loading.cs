//Затемнее по окончанию которой будет загрузка уровня или выход из игры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fon_black_loading : Singleton<Fon_black_loading>
{

    enum Type_level_loading
    {
        Instant,
        Progressbar
    }

    [Header("Тип загрузки")]
    [SerializeField]
    private Type_level_loading Type = Type_level_loading.Instant;

    [Tooltip("Плавность затемнения")]
    [SerializeField]
    float Speed_blackout = 1f;

    [Tooltip("Картинка которая будет затемнятся и осветлятся")]
    [SerializeField]
    Image Image_fon_black = null;

    int Id_number_load_scene = 0;//Номер загружаемой сцены

    string Id_name_load_scene = null;//Имя загружаемой сцены

    bool End = false;//рычаг который показывает конец

    bool Black_up = false;

    float Color_alpha = 1;


    private void FixedUpdate()
    {
        if (!End)
        {
            Active();
        }
    }

    /// <summary>
    /// Активировать
    /// </summary>
    void Active()
    {

            if (Black_up)
            {
                Color_alpha += Speed_blackout * Time.fixedDeltaTime;

                if (Color_alpha >= 1)
                {
                    End = true;
                    Load();
                }
            }
            else
            {
                Color_alpha -= Speed_blackout * Time.fixedDeltaTime;

                if (Color_alpha <= 0)
                    End = true;
            }

            Image_fon_black.color = new Color(0, 0, 0, Color_alpha);

    }

    /// <summary>
    /// Начать загрузку сцены
    /// </summary>
    /// <param name="_number_scene">Номер сцены</param>
    public void Start_loading(int _number_scene)
    {
        Id_number_load_scene = _number_scene;

        Black_up = true;
        End = false;
    }

    /// <summary>
    /// Начать загрузку сцены
    /// </summary>
    /// <param name="_name_scene">Имя сцены</param>
    public void Start_loading(string _name_scene)
    {
        Id_name_load_scene = _name_scene;

        Black_up = true;
        End = false;
    }

    /// <summary>
    /// Выключить игру
    /// </summary>
    public void Exit_game()
    {
        Application.Quit();
    }


    /// <summary>
    /// Загрузка
    /// </summary>
    void Load()
    {
        //Мгновенный вариант загрузки
        if (Type == Type_level_loading.Instant)
        {
            if (Id_name_load_scene != "" && Id_name_load_scene != " " && Id_name_load_scene != null)
                SceneManager.LoadScene(Id_name_load_scene);
            else 
                SceneManager.LoadScene(Id_number_load_scene);
        }

        //Вариант загрузки через загрузочное меню
        else if (Type == Type_level_loading.Progressbar)
        {
            if (Id_name_load_scene == "")
            {
                PlayerPrefs.SetInt("Load_scene_ID", Id_number_load_scene);
                PlayerPrefs.SetString("Load_Id_name_load_scene", "");
            }
            else
                PlayerPrefs.SetString("Load_Id_name_load_scene", Id_name_load_scene);

            PlayerPrefs.Save();

            SceneManager.LoadScene("Loading");
        }

    }
}