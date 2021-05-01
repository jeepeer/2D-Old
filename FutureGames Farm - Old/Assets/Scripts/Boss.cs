using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool isBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isBoss = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isBoss = false;
    }
}
