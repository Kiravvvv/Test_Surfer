//Через время объект с этим скриптом самоуничтожится
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Lifetime_object : MonoBehaviour
{

    [Tooltip("Время до самоуничтожения")]
    [SerializeField]
    float Time_destroy = 10f;

    private void OnEnable()
    {
        StartCoroutine(Destroy_coroutine());
    }


    IEnumerator Destroy_coroutine()
    {
        yield return new WaitForSeconds(Time_destroy);
        LeanPool.Despawn(gameObject);
    }
}
