//���������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    [Tooltip("����� ������ ����")]
    [SerializeField]
    Transform Fire_point = null;

    [Tooltip("������ ����")]
    [SerializeField]
    GameObject Bullet_prefab = null;

    /// <summary>
    /// ����������
    /// </summary>
    public void Fire()
    {
        Instantiate(Bullet_prefab, Fire_point.position, Fire_point.rotation);
    }

}
