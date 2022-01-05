//Управление камерой
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    [Tooltip("Цель для слежения")]
    public Transform Target = null;

    [Tooltip("Смещение камеры")]
    public Vector3 Offset = Vector3.zero;

    [Tooltip("Скорость слежения камеры")]
    public float Speed = 12;

    [Tooltip("Режим редактирования")]
    [SerializeField]
    bool Debug_mode = false;

    void LateUpdate()
    {
        Movement_to_target();
    }

    /// <summary>
    /// Двигает камеру к цели
    /// </summary>
    void Movement_to_target()
    {
        if (Target)
        {

            Vector3 target_position = new Vector3(0, 0, Target.position.z) + Offset;
            transform.position = target_position;
             //transform.position = Vector3.Lerp(transform.position, target_position, Time.fixedDeltaTime * Speed);
        }
    }


    private void OnDrawGizmos()
    {
        if (Target && Debug_mode)
        {
            transform.position = new Vector3(0, 0, Target.position.z) + Offset;
        }
    }

}
