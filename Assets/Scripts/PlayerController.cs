using System;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float rayMaxDistance = 10f;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] Transform objectPrefab;
    [SerializeField] MRUKAnchor.SceneLabels sceneLabels;
    private InputActionsPlayer inputActionsPlayer;

    private void OnEnable()
    {
        inputActionsPlayer = new InputActionsPlayer();
        inputActionsPlayer.Enable();
        inputActionsPlayer.Player.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        inputActionsPlayer.Disable();
        inputActionsPlayer.Player.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext inputContext)
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if (room)
        {
            Ray ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
            LabelFilter labelFilter = new LabelFilter(sceneLabels);
            if (room.Raycast(ray, rayMaxDistance, labelFilter, out RaycastHit hit, out MRUKAnchor anchor))
            {
                Debug.Log("Hit");
                Instantiate(objectPrefab, hit.point, Quaternion.identity, anchor.transform);
            }
        }
    }
}