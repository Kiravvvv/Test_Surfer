//������ ������������ ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_object : MonoBehaviour
{
 
    /// <summary>
    /// ���������� ������
    /// </summary>
    public void Destroy_obj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// ���������� ������ ����� �������� �����
    /// </summary>
    /// <param name="_time">�����</param>
    public void Destroy_timer_obj(float _time)
    {
        Destroy(gameObject);
    }

}
