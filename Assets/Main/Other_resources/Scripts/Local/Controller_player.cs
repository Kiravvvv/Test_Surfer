//Скрипт контроллера
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_player : Singleton<Controller_player>
{
    /// <summary>
    /// Делегат поворота камеры
    /// </summary>
    /// <param name="_vertical">Вертикальный поворот</param>
    /// <param name="_horizontal">Горизонтальный поворот</param>
    public delegate void Rotation_camera_delegate(float _vertical, float _horizontal);

    /// <summary>
    /// Переменная Делегата поворота камеры
    /// </summary>
    public Rotation_camera_delegate Rotation_camera_d = null;

    /// <summary>
    /// Делегат направления движения
    /// </summary>
    /// <param name="_vertical">Направление вперёд</param>
    /// <param name="_horizontal">Направление вбок</param>
    public delegate void Move_delegate(int _vertical, int _horizontal);

    /// <summary>
    /// Переменная Делегата задающая направление движения
    /// </summary>
    public Move_delegate Move_d = null;

    /// <summary>
    /// Делегат прыжка
    /// </summary>
    public delegate void Jump_delegate();

    /// <summary>
    /// Переменная Делегата прыжка
    /// </summary>
    public Jump_delegate Jump_d = null;

    /// <summary>
    /// Делегат Атаки
    /// </summary>
    public delegate void Attack_delegate();

    /// <summary>
    /// Переменная Делегата Атаки
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
    /// Передвижение
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
    /// Поворот камеры
    /// </summary>
    void Camera_rotation()
    {
        float vertical = Input.GetAxis("Mouse Y");
        float horizontal = Input.GetAxis("Mouse X");

        Rotation_camera_d?.Invoke(vertical, horizontal);
    }
}
