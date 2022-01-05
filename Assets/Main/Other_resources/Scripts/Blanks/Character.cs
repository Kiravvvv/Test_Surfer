//Скрипт персонажа
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Character : Game_character
{

    [Tooltip("Высота прыжка")]
    [SerializeField]
    float Jump_height = 4f;

    [Tooltip("Продолжительность прыжка")]
    [SerializeField]
    float Jump_duration = 0.5f;

    [Tooltip("Кривая прыжка")]
    [SerializeField]
    AnimationCurve Jump_curve = new AnimationCurve();

    [Tooltip("Камера")]
    [SerializeField]
    Camera_control Cam_script = null;

    [Tooltip("Персонажа контролирует игрок")]
    [SerializeField]
    bool Player_controller = false;

    protected override void Start()
    {
        base.Start();

        if (Player_controller)
        {
            if (GetComponent<Controller_player>())
            {
                Change_control(GetComponent<Controller_player>());
            }
            else
            {
                Change_control(Controller_player.Instance);
            }
        }
    }

    void Movement(int _vertical, int _horizontal)
    {
        Vector3 direction = Cam_script.transform.forward * _vertical + Cam_script.transform.right * _horizontal;

        direction *= Speed_movement;

        if(_vertical != 0 || _horizontal != 0)
        {
            Body.MovePosition(transform.position + direction);
            Anim.SetBool("Walk", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
        }
        

        Rotation_to_camera();
    }

    void Rotation_to_camera()//Повернуться к камере
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Cam_script.transform.rotation, Speed_rotation);
    }

    void Jump()
    {
        if(Body.velocity.y == 0)
            Grounded = true;

        if (Grounded)
        {
            Grounded = false; 
            StartCoroutine(Jump_coroutine());
            Anim.Play("Jump");
        }
           
       // Body.AddForce(Vector3.up * Jump_force);
    }

    IEnumerator Jump_coroutine()
    {
        float expired_seconds = 0f;
        float progress = 0;
        Vector3 start_position = transform.position;

        while(progress < 1)
        {
            expired_seconds += Time.deltaTime;
            progress = expired_seconds / Jump_duration;

            //transform.position = Vector3.Lerp(start_position, new Vector3(0, Jump_curve.Evaluate(progress) * Jump_height, 0 ), progress);
            transform.position = new Vector3(start_position.x, start_position.y + Jump_curve.Evaluate(progress) * Jump_height, start_position.z);

            yield return null;
        }

        

    }

    void Attack()
    {

    }

    /// <summary>
    /// Указать контроллер игрока
    /// </summary>
    /// <param name="_controller_script">Скрипт контроллера</param>
    public void Change_control(Controller_player _controller_script)
    {
        _controller_script.Move_d += Movement;
        _controller_script.Attack_d += Attack;
        _controller_script.Jump_d += Jump;

        //Cam_script.Connect_controll(_controller_script);
    }

}
