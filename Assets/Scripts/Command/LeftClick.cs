using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;


public class LeftClick : MonoBehaviour
{
    public static LeftClick instance;

    private Camera cam;
    

    [SerializeField]
    private LayerMask layerMask;

    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground" , "Character" , "Building" , "Item");
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClearEverything();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            TrySelect(Input.mousePosition);
        }
    }

    private void SelectCharacter(RaycastHit hit) 
    {
        Characters hero = hit.collider.GetComponent<Characters>();
        Debug.Log("Selected Char : " + hit.collider.gameObject);

        
        PartyManager.instance.SelectChars.Add(hero);
        hero.ToggleRingSelection(hero);
    }

    private void TrySelect(Vector2 screenPos) 
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask)) 
        {
            switch (hit.collider.tag) 
            {
                case "Player":
                case "Hero":
                    SelectCharacter(hit);
                    break;
            }
        }
    }

    private void ClearRingSelection() 
    {
        foreach (Characters h in PartyManager.instance.SelectChars)
            h.ToggleRingSelection(false);
    }

    private void ClearEverything() 
    {
        ClearRingSelection();
        PartyManager.instance.SelectChars.Clear();
    }

    
}
