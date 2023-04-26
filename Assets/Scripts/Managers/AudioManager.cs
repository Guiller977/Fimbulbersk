using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;

    //Referencias a la m�sica del juego
    public AudioSource bgm, levelEndMusic;

    //Hacemos el Singleton de este script
    public static AudioManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //M�todo para reproducir los sonidos
    public void PlaySFX(int soundToPlay)
    {
        //Si ya se estaba reproduciendo este sonido, lo paramos
        soundEffects[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        if (soundToPlay == 13)
        {
            soundEffects[soundToPlay].pitch = 0.4f;
        }

        else soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);        
        //Reproducir el sonido pasado por par�metro
        soundEffects[soundToPlay].Play();
    }
}
