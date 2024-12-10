using System;
using Oculus.Interaction;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ActiveStateSelector fingerOne;
    [SerializeField] private ActiveStateSelector fingerTwo;
    [SerializeField] private ActiveStateSelector fingerThree;
    [SerializeField] private AudioBeatSpawner audioBeatSpawner;

    private bool isFingerOneSelected;
    private bool isFingerTwoSelected;
    private void OnEnable()
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
        audioBeatSpawner.PlaySong();
    }
}
