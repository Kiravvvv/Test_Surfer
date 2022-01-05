using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [Tooltip(" оличество удал€емых блоков у игрока")]
    [SerializeField]
    int Delete_player_surf_block = 1;

    bool Active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Surf_block>() && Active)
        {
            Player.Instance.Delete_surf_block(Delete_player_surf_block);
            Active = false;
        }
    }


}
