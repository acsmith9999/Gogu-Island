using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public AudioSource src;
    //public List<AudioClip> baki, bago, duki, dugo;
    public AudioClip successSound, failSound, helpSound;
    public AudioDirections baki, bago, duki, dugo;

    private void Awake()
    {
        //baki.Add((AudioClip)Resources.Load("Sounds/Bridget/Baki"));
        //baki.Add((AudioClip)Resources.Load("Sounds/Bridget/Baki2"));
        //baki.Add((AudioClip)Resources.Load("Sounds/Bridget/Baki3"));
        //baki.Add((AudioClip)Resources.Load("Sounds/Bridget/Baki4"));

        //bago.Add((AudioClip)Resources.Load("Sounds/Bridget/Bago"));
        //bago.Add((AudioClip)Resources.Load("Sounds/Bridget/Bago2"));
        //bago.Add((AudioClip)Resources.Load("Sounds/Bridget/Bago3"));
        //bago.Add((AudioClip)Resources.Load("Sounds/Bridget/Bago4"));

        //duki.Add((AudioClip)Resources.Load("Sounds/Bridget/Duki"));
        //duki.Add((AudioClip)Resources.Load("Sounds/Bridget/Duki2"));
        //duki.Add((AudioClip)Resources.Load("Sounds/Bridget/Duki3"));
        //duki.Add((AudioClip)Resources.Load("Sounds/Bridget/Duki4"));

        //dugo.Add((AudioClip)Resources.Load("Sounds/Bridget/Dugo"));
        //dugo.Add((AudioClip)Resources.Load("Sounds/Bridget/Dugo2"));
        //dugo.Add((AudioClip)Resources.Load("Sounds/Bridget/Dugo3"));
        //dugo.Add((AudioClip)Resources.Load("Sounds/Bridget/Dugo4"));

        //successSound = (AudioClip)Resources.Load("Sounds/Success");
        //failSound = (AudioClip)Resources.Load("Sounds/Fail");

    }
    private void Start()
    {
        src = GetComponent<AudioSource>();
        if(Parameters.helpGender == 0)
        {
            helpSound = (AudioClip)Resources.Load("Sounds/MaleHelp");
        }
        else if(Parameters.helpGender == 1)
        {
            helpSound = (AudioClip)Resources.Load("Sounds/FemaleHelp");
        }
        else { Debug.Log("no help gender specified"); }

    }
}
