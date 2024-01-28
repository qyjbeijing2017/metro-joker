using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkillVideoPlayer : MonoBehaviour
{
    private Queue<string> videosToPlay = new();
    [SerializeField] private VideoPlayer player;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private RawImage image;

    private void Start()
    {
        player.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        source.Stop();
        TryPlayNext();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySkill("");
        }
    }

    private void OnEnable()
    {
        EventManager.RegisterCallback<string>(EventName.PlaySkill, PlaySkill);
    }

    private void OnDisable()
    {
        EventManager.UnregisterCallback<string>(EventName.PlaySkill, PlaySkill);
    }

    private void PlaySkill(string name)
    {
        videosToPlay.Enqueue(name);
        TryPlayNext();
    }

    private void TryPlayNext()
    {
        if (player.isPlaying)
            return;

        if (videosToPlay.Count <= 0)
            return;

        var str = videosToPlay.Dequeue();
        player.clip = Resources.Load<VideoClip>($"videos/{str}");
    }

    private IEnumerator Tween(bool isIn)
    {
        var start = isIn ? new Color(1, 1, 1, 0) : Color.white;
        var end = isIn ? Color.white : new Color(1, 1, 1, 0);
        image.color = start;
        float counter = fadeInTime;
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            image.color = Color.Lerp(start, end, 1 - counter / fadeInTime);
            yield return null;
        }

        image.color = end;
        if (!isIn)
        {
            TryPlayNext();
        }
        else
        {
            player.Play();
        }
    }
}