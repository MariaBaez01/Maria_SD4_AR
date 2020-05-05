﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class videoPlayerManager : MonoBehaviour
{
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    public GameObject myimage;

    public UnityEngine.Video.VideoClip videoClip;

    void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        var audioSource = gameObject.AddComponent<AudioSource>();

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


        if (CrossPlatformInputManager.GetButtonDown("Sound"))
        {
            var vp = GetComponent<UnityEngine.Video.VideoPlayer>();

            if (vp.isPlaying && isMuted == false)
            {
                vp.SetDirectAudioMute(0, !isMuted);
                isMuted = true;
                myimage.GetComponent<Image>().sprite = musicOnImage;
            }
            else
            {
                vp.SetDirectAudioMute(0, false);
                isMuted = false;
                myimage.GetComponent<Image>().sprite = musicOffImage;
            }
        }

    }
}