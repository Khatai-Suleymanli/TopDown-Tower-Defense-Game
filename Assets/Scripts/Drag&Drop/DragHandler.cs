using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragHandler : MonoBehaviour, IPointerDownHandler
{
    public GameObject turretPrefab;
    private GameObject previewTurret;
    private bool isDragging = false;
    public LayerMask placementLayer;
    private Camera mainCamera;
    private IdleGoldCounter goldCounter;




    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        placementLayer = LayerMask.GetMask("Ground");
        goldCounter = FindObjectOfType<IdleGoldCounter>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (goldCounter.CanAffordTurret()) // Only start drag if player can afford
        {
            StartDrag();
        } // implement down ----------
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) { 
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
            {
                previewTurret.transform.position = hit.point;
            }

            if (Input.GetMouseButtonUp(0)) {
                EndDrag(); /// impelmeny
            
            }
        }
    }

    void StartDrag()
    {
        isDragging = true;
        previewTurret = Instantiate(turretPrefab);

        // Disable components
        foreach (var component in previewTurret.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }

        foreach (var collidier in previewTurret.GetComponents<Collider>())
        {
            collidier.enabled = false;
        }

        // Modify materials for all renderers
        foreach (var renderer in previewTurret.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.blue;
        }
    }


    void EndDrag()
    {
        Ray ray =  mainCamera.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            if (goldCounter.CanAffordTurret())
            {
                Instantiate(turretPrefab, hit.point, Quaternion.identity);
                goldCounter.buyTurrette();
            }
        }

        Destroy(previewTurret);
        isDragging = false;
    }
}
