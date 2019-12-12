using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.Video;
using Toggle = UnityEngine.UI.Toggle;

public class StreamVideo : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Toggle toggleDisableAudio;
    [SerializeField] private Toggle toggleDisableMedia;
    private void Start()
    {
        StartCoroutine(PlayVideo());
        var labelAudio = toggleDisableAudio.transform.Find("Label");
        var labelMedia = toggleDisableMedia.transform.Find("Label");
        labelAudio.GetComponent<Text>().text = "Tắt tiếng";
        labelMedia.GetComponent<Text>().text = "Tắt hoạt ảnh";
    }

    private void Update()
    {
        videoPlayer.SetDirectAudioMute(0, toggleDisableAudio.isOn);
        if (toggleDisableMedia.isOn)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
}
