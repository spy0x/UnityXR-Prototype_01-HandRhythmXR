using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ActiveStateSelector fingerOne;
    [SerializeField] private ActiveStateSelector fingerTwo;
    [SerializeField] private ActiveStateSelector fingerThree;
    [SerializeField] private AudioBeatSpawner audioBeatSpawner;
    [SerializeField] UnityEvent onSongStart;

    private bool isFingerOneSelected;
    private bool isFingerTwoSelected;
    private void OnEnable()
    {
        BeatSpawnerPlaceSystem.OnBeatSpawnerPlaced += SetFingerSelectors;
    }

    private void OnDisable()
    {
        BeatSpawnerPlaceSystem.OnBeatSpawnerPlaced -= SetFingerSelectors;
    }

    private void SetFingerSelectors()
    {
        fingerOne.WhenSelected += OnFingerOneSelected;
        fingerTwo.WhenSelected += OnFingerTwoSelected;
        fingerThree.WhenSelected += OnFingerThreeSelected;
    }

    private void OnFingerOneSelected()
    {
        isFingerOneSelected = true;
    }

    private void OnFingerTwoSelected()
    {
        if (!isFingerOneSelected) return;
        isFingerTwoSelected = true;
    }

    private void OnFingerThreeSelected()
    {
        if (!isFingerTwoSelected || !isFingerTwoSelected) return;
        DisableFingerSelectors();
        StartGame();
    }

    private void DisableFingerSelectors()
    {
        fingerOne.enabled = false;
        fingerTwo.enabled = false;
        fingerThree.enabled = false;
    }

    private void StartGame()
    {
        onSongStart.Invoke();
        audioBeatSpawner.PlaySong();
    }
}
