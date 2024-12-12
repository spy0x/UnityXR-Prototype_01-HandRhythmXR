using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SongTimeDisplay : MonoBehaviour
{
    [SerializeField] Slider songTimeSlider;
    [SerializeField] TextMeshProUGUI songTimeText;
    private PlayableDirector songTimeline;

    public void SetSongDuration(PlayableDirector song)
    {
        songTimeline = song;
        songTimeSlider.gameObject.SetActive(true);
    }

    void Start()
    {
        songTimeText.text = String.Empty;
        songTimeSlider.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!songTimeline) return;
        songTimeSlider.value = (float)songTimeline.time / (float)songTimeline.duration;
        songTimeText.text = $"{Mathf.FloorToInt((float)songTimeline.time / 60):00}:{Mathf.FloorToInt((float)songTimeline.time % 60):00} / {Mathf.FloorToInt((float)songTimeline.duration / 60):00}:{Mathf.FloorToInt((float)songTimeline.duration % 60):00}";
    }
}
