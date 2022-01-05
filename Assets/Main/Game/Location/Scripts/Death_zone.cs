//Зона в которой всё уничтожается
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_zone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().Damage_add(999999, null);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
