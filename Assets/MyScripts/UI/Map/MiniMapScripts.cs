using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScripts : RPGMonoBehaviour
{
    private GameObject player;

    void Start()
    {
        StartCoroutine(FindPlayer());
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            playerPos.y = transform.position.y;
            transform.position = playerPos;
        }

    }
    public IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1);
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
