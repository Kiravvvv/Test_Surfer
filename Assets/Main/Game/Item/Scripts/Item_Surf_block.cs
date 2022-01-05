using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Surf_block : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Add_surf_block();
            Destroy(gameObject);
        }
    }
}
