using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class videoPlayerManager : MonoBehaviour
{
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    Button soundButton;


    public UnityEngine.Video.VideoClip videoClip;

    void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        var audioSource = gameObject.AddComponent<AudioSource>();
        soundButton = GameObject.Find("SoundButton").GetComponent<Button>();


        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        

    }
    bool isMuted = false;


    void Update()
    {

        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        if (CrossPlatformInputManager.GetButtonDown("Sound"))
        {


            if (vp.isPlaying && isMuted == false)
            {
                vp.SetDirectAudioMute(0, !isMuted);
                isMuted = true;

                //soundButton.image.sprite = musicOnImage;
            }
            else
            {
                vp.SetDirectAudioMute(0, false);
                isMuted = false;
                //soundButton.image.sprite = musicOffImage;
            }


        }

        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 1)
                {

                    if (vp.isPlaying)
                    {
                        vp.Pause();
                    }
                    else
                    {
                        vp.Play();
                    }

                }
                else if (Input.GetTouch(i).tapCount == 2)
                {

                    vp.Stop();
                    vp.Play();

                }
            }
        }

    }
    


}
