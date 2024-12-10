using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource;  // AudioSource dla intro
    public AudioSource loopSource;   // AudioSource dla p�tli (loop)
    private double goalTime;

    private void Start()
    {
        // Ustawienie czasu rozpocz�cia intro
        goalTime = AudioSettings.dspTime + 0.5;  // Opcjonalne op�nienie o 0.5 sekundy
        introSource.PlayScheduled(goalTime);

        // Oblicz czas trwania intro i zaplanuj rozpocz�cie loopa po jego zako�czeniu
        double introDuration = (double)introSource.clip.samples / introSource.clip.frequency;
        loopSource.PlayScheduled(goalTime + introDuration);

        // Ustawienie loopSource na odtwarzanie w p�tli
        loopSource.loop = true;
    }
}
