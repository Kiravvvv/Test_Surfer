//Скрипт уничтожающий объект
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_object : MonoBehaviour
{
 
    /// <summary>
    /// Уничтожить объект
    /// </summary>
    public void Destroy_obj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Уничтожить объект через указаное время
    /// </summary>
    /// <param name="_time">Время</param>
    public void Destroy_timer_obj(float _time)
    {
        Destroy(gameObject);
    }

}
