using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Tooltip("������")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("��������")]
    [SerializeField]
    Animator Anim = null;

    [Tooltip("�������� ��������")]
    [SerializeField]
    float Speed_movement = 1f;

    [Tooltip("�������� �������� ����")]
    [SerializeField]
    float Speed_movement_sideways = 0.2f;

    [Tooltip("������ ����� ��� ����")]
    [SerializeField]
    GameObject Surf_block_prefab = null;

    [Tooltip("��������� ����� �������")]
    [SerializeField]
    float Distance_new_surf_block = 0.5f;

    [Tooltip("������������ ���������� �� ������")]
    [SerializeField]
    float Max_distance_sideways = 4.4f;

    [Tooltip("���� �������� ������ �� ������� ����� �����")]
    [SerializeField]
    List<Transform> Surf_block_list = new List<Transform>();

    [Tooltip("������ ��������")]
    [SerializeField]
    Ragdoll_active Ragdol_script = null;

    [Tooltip("������")]
    [SerializeField]
    Camera Cam = null;

    [Tooltip("����� �����")]
    [SerializeField]
    Transform Trail = null;

    [Tooltip("������� ������ ��� ������� ������")]
    [SerializeField]
    ParticleSystem PS = null;

    int Movement_right = 1;//�������� ������

    bool Movement_bool = false;//��������� ��������

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
    /// ������������
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
    /// ��������
    /// </summary>
    void Death()
    {
        Stop();
        Ragdol_script.Active_change(true);
        Game_administrator.Instance.End_game(false);
    }


    /// <summary>
    /// �������� ������ ��� ���������
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
    /// ������� ���� � ������
    /// </summary>
    /// <param name="_amount">���������� ��������� ������</param>
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
    /// ������ ��������
    /// </summary>
    public void Start_game()
    {
        Movement_bool = true;
    }

    /// <summary>
    /// ���������� ������
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
