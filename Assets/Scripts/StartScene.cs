using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject player;
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
            p = new Vector3(248.5f, 30, 375); q = new Quaternion(0, 150, 0, 0);
            Instantiate(player, p, q);
        }
        
        
        
    }

    
}
