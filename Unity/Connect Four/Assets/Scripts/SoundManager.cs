using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static SoundManager sm;
    private AudioSource ausc;

    void Awake()
    {
        sm = this;
        ausc = GetComponent<AudioSource>();
    }

    public static void PlaySound(Sounds sou, float volume = 1)
    {
        sm.ausc.PlayOneShot(sm.sounds[(int)sou], volume);
    }
}

public enum Sounds 
{ 
    CoinToss,
    CoinRes,
    Shoot,
    BallInPlace,
    Victory 
}