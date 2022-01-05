//Триггер нанесения урона
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_damage : MonoBehaviour
{

    [Tooltip("Здоровье владельца")]
    [SerializeField]
    Health My_health = null;

    [Tooltip("Нанесения урона")]
    [SerializeField]
    int Damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Health h = null;

        if (other.GetComponent<Health>())
            h = other.GetComponent<Health>();

        if (h != null && h != My_health)
        {
            h.Damage_add(Damage, null);
        }
    }
}
