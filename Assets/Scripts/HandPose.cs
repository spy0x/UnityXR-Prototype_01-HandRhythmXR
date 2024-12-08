using System;
using Oculus.Interaction;
using UnityEngine;

public class HandPose : MonoBehaviour
{
    [SerializeField] float moveSpeed = .5f;
    [SerializeField] Vector3 moveDirection = Vector3.up;
    [SerializeField] ActiveStateSelector activeStateSelector;
    [SerializeField] Renderer meshRenderer;
    [SerializeField] Material goodMaterial;
    [SerializeField] Material badMaterial;

    private bool hasBeenSelected = false;
    private void OnEnable()
    {
        activeStateSelector.WhenSelected += OnSelected;
    }

    private void Start()
    {
        activeStateSelector.enabled = false;
    }

    private void OnDisable()
    {
        activeStateSelector.WhenSelected -= OnSelected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            activeStateSelector.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            activeStateSelector.enabled = false;
            if(!hasBeenSelected) meshRenderer.material = badMaterial;
            Destroy(gameObject, 2f);
        }
    }

    private void OnSelected()
    {
        hasBeenSelected = true;
        activeStateSelector.enabled = false;
        meshRenderer.material = goodMaterial;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3(moveDirection.x * moveSpeed * Time.deltaTime, moveDirection.y * moveSpeed * Time.deltaTime, moveDirection.z * moveSpeed * Time.deltaTime);
    }
    
}
