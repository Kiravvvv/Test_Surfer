using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_tap : MonoBehaviour
{


    /// <summary>
    /// Начать игру
    /// </summary>
    public void Tap()
    {
        Player.Instance.Start_game();
    }

}
