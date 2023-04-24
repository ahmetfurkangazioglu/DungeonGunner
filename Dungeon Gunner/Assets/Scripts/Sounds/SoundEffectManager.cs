using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonobehaviour<SoundEffectManager>
{
    public int soundsVolume = 8;
    private SoundEffect sound;
    private SoundEffect forceStopsound;
    private SoundEffect unstoppableSound;
    private void Start()
    {
        SetSoundsVolume(soundsVolume);
    }

    /// <summary>
    /// Play the sound effect
    /// </summary>
    public void PlaySoundEffect(SoundEffectSO soundEffect, bool isUnstoppableSound)
    {
        // Play sound using a sound gameobject and component from the object pool
        if (isUnstoppableSound)
        {
            unstoppableSound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.soundPrefab, Vector3.zero, Quaternion.identity);
            sound = unstoppableSound;
        }
        else
        {
            forceStopsound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.soundPrefab, Vector3.zero, Quaternion.identity);
            sound = forceStopsound;
        }
   
        sound.SetSound(soundEffect);
        sound.gameObject.SetActive(true);
        StartCoroutine(DisableSound(sound, soundEffect.soundEffectClip.length));

    }


    /// <summary>
    /// Disable sound effect object after it has played thus returning it to the object pool
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        sound.gameObject.SetActive(false);
    }

    public void ForceDisableSound()
    {
        if (forceStopsound != null)
        {
            forceStopsound.gameObject.SetActive(false);
            forceStopsound = null;
         
        }
    }
    /// <summary>
    /// Set sounds volume
    /// </summary>
    private void SetSoundsVolume(int soundsVolume)
    {
        float muteDecibels = -80f;

        if (soundsVolume == 0)
        {
            GameResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        }
        else
        {
            GameResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", HelperUtilities.LinearToDecibels(soundsVolume));
        }
    }
}