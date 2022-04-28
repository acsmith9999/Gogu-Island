using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject player, spawnPoint;
    private Vector3 p;
    private Quaternion q;


    private void Awake()
    {
        Time.timeScale = 1;
        if (SaveManager.activeSave != null)
        {
            p = SaveManager.activeSave.position;
            q = SaveManager.activeSave.rotation;
            Instantiate(player, p, q);
        }
        else if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(player, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        
        
        
    }

    
}
