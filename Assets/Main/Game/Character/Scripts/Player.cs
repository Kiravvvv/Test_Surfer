using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Tooltip("Физика")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Аниматор")]
    [SerializeField]
    Animator Anim = null;

    [Tooltip("Скорость движения")]
    [SerializeField]
    float Speed_movement = 1f;

    [Tooltip("Скорость движения вбок")]
    [SerializeField]
    float Speed_movement_sideways = 0.2f;

    [Tooltip("Префаб блока для сёрфа")]
    [SerializeField]
    GameObject Surf_block_prefab = null;

    [Tooltip("Дистанция между блоками")]
    [SerializeField]
    float Distance_new_surf_block = 0.5f;

    [Tooltip("Максимальное отклонение от центра")]
    [SerializeField]
    float Max_distance_sideways = 4.4f;

    [Tooltip("Лист активных блоков на котором стоит игрок")]
    [SerializeField]
    List<Transform> Surf_block_list = new List<Transform>();

    [Tooltip("Скрипт рэгдолла")]
    [SerializeField]
    Ragdoll_active Ragdol_script = null;

    [Tooltip("Камера")]
    [SerializeField]
    Camera Cam = null;

    [Tooltip("Хвост следа")]
    [SerializeField]
    Transform Trail = null;

    [Tooltip("Система частиц при подборе блоков")]
    [SerializeField]
    ParticleSystem PS = null;

    int Movement_right = 1;//Движение вправо

    bool Movement_bool = false;//Разрешает движение

    Vector3 pos_right = Vector3.zero;

    void Update()
    {
        if (Movement_bool)
            Movement();

        if (Input.GetKey(KeyCode.Mouse0) && Movement_bool)
        {
            float pos_cursor = Cam.ScreenToViewportPoint(Input.mousePosition).x;
            pos_cursor -= 0.5f;

            float new_pos = pos_cursor / 0.5f * Max_distance_sideways;
            pos_right = new Vector3(new_pos, Body.position.y, Body.position.z);

            /*
            if (pos_cursor < -0.2f)
            {
                Movement_right = -1;
            }
            else if(pos_cursor > 0.2f)
            {
                Movement_right = 1;
            }
            else
            {
                Movement_right = 0;
            }
            */
        }
        else
        {
            Movement_right = 0;
        }

    }


    /// <summary>
    /// Передвижение
    /// </summary>
    void Movement()
    {
        /*
        if(Movement_right > 0 && transform.position.x >= Max_distance_sideways)
        Movement_right = 0;

        else if (Movement_right < 0 && transform.position.x <= -Max_distance_sideways)
            Movement_right = 0;
        */

        //Vector3 direction_sideways =  Vector3.right * Movement_right * Speed_movement_sideways;

        Vector3 direction_forward = Vector3.forward * Speed_movement * Time.fixedDeltaTime;


        //Body.MovePosition(transform.position + (direction_forward + direction_sideways));

        Body.MovePosition(new Vector3(pos_right.x, transform.position.y, transform.position.z) + direction_forward);
    }


    /// <summary>
    /// Проигрыш
    /// </summary>
    void Death()
    {
        Stop();
        Ragdol_script.Active_change(true);
        Game_administrator.Instance.End_game(false);
    }


    /// <summary>
    /// Добавить игроку ещё платформу
    /// </summary>
    public void Add_surf_block()
    {
        if (Surf_block_list.Count > 0)
        {
            Vector3 new_position = Surf_block_list[Surf_block_list.Count - 1].transform.position - new Vector3(0, Distance_new_surf_block, 0);

            GameObject obj = Instantiate(Surf_block_prefab, new_position, Quaternion.identity, transform);

            Surf_block_list.Add(obj.transform);

            PS.Play();

            Vector3 new_pos_trail = Surf_block_list[Surf_block_list.Count - 1].position;
            new_pos_trail.y -= 0.2f;
            Trail.position = new_pos_trail;
        }

    }


    /// <summary>
    /// Удалить блок у игрока
    /// </summary>
    /// <param name="_amount">Количество удаляемых блоков</param>
    public void Delete_surf_block(int _amount)
    {
        for (int x = 0; x < _amount; x++)
        {
            if (Surf_block_list.Count == 0)
            {
                break;
            }
            else
            {
                Transform obj = Surf_block_list[Surf_block_list.Count - 1];
                Surf_block_list.Remove(obj);
                obj.gameObject.transform.SetParent(null);
                obj.GetComponent<Renderer>().material.color = Color.white;

                if (Surf_block_list.Count == 0)
                {
                    Death();
                }

                Vector3 new_pos_trail = Surf_block_list[Surf_block_list.Count - 1].position;
                new_pos_trail.y -= 0.2f;
                Trail.position = new_pos_trail;
            }
        }
    }

    /// <summary>
    /// Начать движение
    /// </summary>
    public void Start_game()
    {
        Movement_bool = true;
    }

    /// <summary>
    /// Остановить игрока
    /// </summary>
    public void Stop()
    {
        Movement_bool = false;
        Anim.Play("Dance");
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(transform.position + new Vector3(Max_distance_sideways, 0, 0), Vector3.one);
        Gizmos.DrawCube(transform.position + new Vector3(-Max_distance_sideways, 0, 0), Vector3.one);
    }

}
