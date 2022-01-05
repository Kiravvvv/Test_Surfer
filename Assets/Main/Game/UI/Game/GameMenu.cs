//Скрипт для игрового меню во время паузы
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : Singleton<GameMenu> {

    [Header("Название сцены которая ведёт в главное меню")]
    [SerializeField]
    string Scene_main_menu = "Main_menu";

    [Header("Заранее записаная загрузка уровня")]
    [SerializeField]
    string Load_scene_name = "Menu"; 

    [Header("Клавиша вкл/выкл меню ")]
    [SerializeField]
    KeyCode Exit_key = KeyCode.Escape;

    [Header("Ссылки на игровые вкладки")]
    [SerializeField]
    List<GameObject> Bookmark = new List<GameObject>();

    [Header("Будет влиять на курсор во время включения и выключения меню")]
    [SerializeField]
    bool Cursor_active = false;

    void Update()
    {
        if (Input.GetKeyDown(Exit_key))
        {
            Enter_button();
        }

    }

    void Enter_button()//Включение и отключение игрового меню
    {
        if (!Interface_administrator.Instance.Game_menu_bool)
        {
            Interface_administrator.Instance.Game_menu_active(true);

            if (Cursor_active)
                Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            bool null_bookmark = true;

            for (int x = 0; x < Bookmark.Count; x++)
            {
                if (Bookmark[x].activeSelf)
                {
                    null_bookmark = false;
                    Bookmark[x].SetActive(false);
                }
            }
            if (null_bookmark)
            {
                Interface_administrator.Instance.Game_menu_active(false);

                if (Cursor_active)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    //Продолжить дальше (выключить меню)
    public void Continue()
    {
        Interface_administrator.Instance.Game_menu_active(false);
        Time.timeScale = 1;

        if(Cursor_active)
        Game_Player.Cursor_player(false);
    }

    //Перезагрузить сцену (уровень)
    public void Restart_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //Выход в главное меню (загрузить сцену с главным меню)
    public void Load_main_menu(){
        Time.timeScale = 1;
        Fon_black_loading.Instance.Start_loading(Scene_main_menu);
        Interface_administrator.Instance.Game_menu_active(false);
    }

    public void Load_scene()//Загрузить указанную тут сцену (уровень)
    {
        Fon_black_loading.Instance.Start_loading(Load_scene_name);
        Time.timeScale = 1;
        Interface_administrator.Instance.Game_menu_active(false);
    }

    public void Load_scene(int _id_scene)//Загрузить указанную сцену (уровень)
    {
        Fon_black_loading.Instance.Start_loading(_id_scene);
        Time.timeScale = 1;
        Interface_administrator.Instance.Game_menu_active(false);
    }

    public void Load_scene(string _name_scene)//Загрузить указанную сцену (уровень)
    {
        Fon_black_loading.Instance.Start_loading(_name_scene);
        Time.timeScale = 1;
        Interface_administrator.Instance.Game_menu_active(false);
    }

    public void Next_scene()//Загрузить следующую сцену
    {
        int id_scene = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > id_scene)
        {
            Fon_black_loading.Instance.Start_loading(id_scene);
        }
        else
        {
            Debug.Log("Сцены закончились, возвращаемся в главное меню.");
            Load_main_menu();
        }
            
    }

    public void Exit_game()//Выключить игру
    {
        Application.Quit();
    }
		
}
