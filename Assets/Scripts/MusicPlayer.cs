using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource;  // AudioSource dla intro
    public AudioSource loopSource;   // AudioSource dla pêtli (loop)
    private double goalTime;

    private void Start()
    {
        // Ustawienie czasu rozpoczêcia intro
        goalTime = AudioSettings.dspTime + 0.5;  // Opcjonalne opóŸnienie o 0.5 sekundy
        introSource.PlayScheduled(goalTime);

        // Oblicz czas trwania intro i zaplanuj rozpoczêcie loopa po jego zakoñczeniu
        double introDuration = (double)introSource.clip.samples / introSource.clip.frequency;
        loopSource.PlayScheduled(goalTime + introDuration);

        // Ustawienie loopSource na odtwarzanie w pêtli
        loopSource.loop = true;
    }
}
