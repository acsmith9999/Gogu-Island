using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public AudioSource src;
    public AudioClip successSound, failSound, baki, bago, duki, dugo;

    private void Awake()
    {
        baki = (AudioClip)Resources.Load("Sounds/Baki");
        bago = (AudioClip)Resources.Load("Sounds/Bago");
        duki = (AudioClip)Resources.Load("Sounds/Duki");
        dugo = (AudioClip)Resources.Load("Sounds/Dugo");
        successSound = (AudioClip)Resources.Load("Sounds/Success");
        failSound = (AudioClip)Resources.Load("Sounds/Fail");
    }
    private void Start()
    {
        src = GetComponent<AudioSource>();


    }
}
