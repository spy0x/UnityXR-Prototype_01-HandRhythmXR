using System;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class BeatSpawnerPlaceSystem : MonoBehaviour
{
    [SerializeField] Transform beatSpawner;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] float rayMaxDistance = 0.1f;
    [SerializeField] MRUKAnchor.SceneLabels sceneLabels;

    public static event Action OnBeatSpawnerPlaced;
    private MRUKRoom room;
    private MRUKAnchor floorAnchor;

    private bool hasPlaced;
    private void Update()
    {
        if (!room || hasPlaced) return;
        if (transform.localPosition == Vector3.zero) return;
        Ray ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
        LabelFilter labelFilter = new LabelFilter(sceneLabels);
        if (room.Raycast(ray, rayMaxDistance, labelFilter, out RaycastHit hit))
        {
            if (!beatSpawner) return;
            hasPlaced = true;
            beatSpawner.gameObject.SetActive(true);
            beatSpawner.position = hit.point;

            beatSpawner.rotation = Quaternion.LookRotation(-rightHandTransform.position, Vector3.up);

            OnBeatSpawnerPlaced?.Invoke();
        }
    }

    public void SetRoom()
    {
        room = MRUK.Instance.GetCurrentRoom();
    }
}