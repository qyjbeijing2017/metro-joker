using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class SkillVideoPlayer : MonoBehaviour
{
    private Queue<string> videosToPlay = new();
    [SerializeField] private VideoPlayer player;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private VideoClip[] clips;

    private void Start()
    {
        player.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        source.Stop();
        StartCoroutine(Tween(false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySkill("run");
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
        if (clips.All(i => i.name != name))
            return;
        videosToPlay.Enqueue(name);
        TryPlayNext();
    }

    private void TryPlayNext()
    {
        if (player.isPlaying)
            return;

        if (videosToPlay.Count <= 0)
            return;

        StartCoroutine(Tween(true));
    }

    private IEnumerator Tween(bool isIn)
    {
        var start = isIn ? 0 : 1;
        var end = isIn ? 1 : 0;
        cg.alpha = start;
        if (isIn)
        {
            TimeManager.SetTimeScale(0);
        }

        float counter = fadeInTime;
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, 1 - counter / fadeInTime);
            yield return null;
        }

        cg.alpha = end;
        if (!isIn)
        {
            TryPlayNext();
            TimeManager.SetTimeScale(1);
        }
        else
        {
            var str = videosToPlay.Dequeue();
            player.clip = clips.First(i => i.name == str);
            player.Play();
        }
    }
}