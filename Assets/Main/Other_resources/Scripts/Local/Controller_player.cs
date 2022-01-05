//������ �����������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_player : Singleton<Controller_player>
{
    /// <summary>
    /// ������� �������� ������
    /// </summary>
    /// <param name="_vertical">������������ �������</param>
    /// <param name="_horizontal">�������������� �������</param>
    public delegate void Rotation_camera_delegate(float _vertical, float _horizontal);

    /// <summary>
    /// ���������� �������� �������� ������
    /// </summary>
    public Rotation_camera_delegate Rotation_camera_d = null;

    /// <summary>
    /// ������� ����������� ��������
    /// </summary>
    /// <param name="_vertical">����������� �����</param>
    /// <param name="_horizontal">����������� ����</param>
    public delegate void Move_delegate(int _vertical, int _horizontal);

    /// <summary>
    /// ���������� �������� �������� ����������� ��������
    /// </summary>
    public Move_delegate Move_d = null;

    /// <summary>
    /// ������� ������
    /// </summary>
    public delegate void Jump_delegate();

    /// <summary>
    /// ���������� �������� ������
    /// </summary>
    public Jump_delegate Jump_d = null;

    /// <summary>
    /// ������� �����
    /// </summary>
    public delegate void Attack_delegate();

    /// <summary>
    /// ���������� �������� �����
    /// </summary>
    public Attack_delegate Attack_d = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack_d?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump_d?.Invoke();
        }

        Movement();

        Camera_rotation();
    }

    /// <summary>
    /// ������������
    /// </summary>
    void Movement()
    {
        int vertical = 0;
        int horizontal = 0;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }


        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }

         Move_d?.Invoke(vertical, horizontal);
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    void Camera_rotation()
    {
        float vertical = Input.GetAxis("Mouse Y");
        float horizontal = Input.GetAxis("Mouse X");

        Rotation_camera_d?.Invoke(vertical, horizontal);
    }
}
