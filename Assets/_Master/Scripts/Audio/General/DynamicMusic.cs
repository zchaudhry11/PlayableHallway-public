using UnityEngine;
using UnityEngine.Audio;

public class DynamicMusic : MonoBehaviour
{
    [SerializeField] private AudioClip firstSong;
    [SerializeField] private AudioClip secondSong;
    private float currTime = 0;

    [SerializeField] private float blendDelay = 0.5f; // Time to wait before beginning blend
    [SerializeField] private float blendDuration = 1.0f; // Speed multiplier for audio blending.
    private float blendSpeed;
    private float blendTimer = 0;
    private bool blended = false;

    private AudioSource audSrcPrim;
    private AudioSource audSrcSec;
    private AudioMixerGroup bgmMixer;

    private void Start()
    {
        audSrcPrim = this.transform.GetChild(0).GetComponent<AudioSource>();
        audSrcSec = this.transform.GetChild(1).GetComponent<AudioSource>();
        bgmMixer = audSrcPrim.outputAudioMixerGroup;

        blendSpeed = Time.deltaTime;

        // TODO: Handle BGM in a separate audio manager
        audSrcPrim.clip = firstSong;
        audSrcPrim.outputAudioMixerGroup = null;
        audSrcSec.clip = secondSong;
        audSrcSec.outputAudioMixerGroup = null;

        audSrcPrim.Play();
        audSrcSec.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            BlendMusic();
            print("bgm playing from dynamicmusic class");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseBGMHighPass();
        }
    }

    private void FixedUpdate()
    {
        // Lower volume of first song and raise volume of second
        if (blended)
        {
            if (blendTimer > 0)
            {
                blendTimer -= Time.deltaTime;
            }
            else
            {
                // Blend songs
                if (audSrcPrim.volume > 0)
                {
                    audSrcPrim.volume -= blendSpeed * blendDuration;
                }

                if (audSrcSec.volume < 1)
                {
                    audSrcSec.volume += blendSpeed * blendDuration;
                }
            }
        }
        else // Lower volume of second song and raise volume of first song
        {
            if (blendTimer > 0)
            {
                blendTimer -= Time.deltaTime;
            }
            else
            {
                // Blend songs
                if (audSrcSec.volume > 0)
                {
                    audSrcSec.volume -= blendSpeed * blendDuration;
                }

                if (audSrcPrim.volume < 1)
                {
                    audSrcPrim.volume += blendSpeed * blendDuration;
                }
            }
        }
    }

    /// <summary>
    /// Transitions between two different songs.
    /// </summary>
    private void BlendMusic()
    {
        if (audSrcPrim && audSrcSec)
        {
            // Switch to second song
            if (!blended)
            {
                if (secondSong)
                {
                    blended = true;
                    blendTimer = blendDelay;
                }
            }
            else // Switch back to first song
            {
                if (firstSong)
                {
                    blended = false;
                    blendTimer = blendDelay;
                }
            }
        }
        else
        {
            Debug.LogError("No audiosource was found!");
        }
    }

    /// <summary>
    /// Enable or disable the BGM high pass filter. Used when player takes damage and other effects.
    /// </summary>
    private void UseBGMHighPass()
    {
        // If a mixer group is enabled, disable it
        if (audSrcPrim.outputAudioMixerGroup)
        {
            audSrcPrim.outputAudioMixerGroup = null;
            audSrcSec.outputAudioMixerGroup = null;
        }
        else // If a mixer group isn't present, add the BGM mixer;
        {
            audSrcPrim.outputAudioMixerGroup = bgmMixer;
            audSrcSec.outputAudioMixerGroup = bgmMixer;
        }
    }
}