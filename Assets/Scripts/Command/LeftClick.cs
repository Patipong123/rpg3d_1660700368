using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LeftClick : MonoBehaviour
{
    public static LeftClick instance;

    // Event Driven: fired when an Item in the world is clicked
    public static event Action<ItemPick> OnItemClicked;

    private Camera cam;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private RectTransform boxSelection;
    private Vector2 oldAnchoredPos;
    private Vector2 startPos;

    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building", "Item");
        boxSelection = UIManager.instance.SelectionBox;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPos = Mouse.current.position.ReadValue();

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            ClearEverything();
        }

        if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionBox(Mouse.current.position.ReadValue());
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            ReleaseSelectionBox(mousePos);
            TrySelect(mousePos);
        }
    }

    private int SelectCharacter(RaycastHit hit)
    {
        ClearEverything();
        Characters hero = hit.collider.GetComponent<Characters>();

        int i = PartyManager.instance.FindIndexFromClass(hero);
        UIManager.instance.ToggleAvatar[i].isOn = true;
        return i;
    }

    // Fix: LeftClick now handles Item pickup via raycast (OnMouseDown ไม่ทำงานกับ New Input System)
    public void SelectItem(RaycastHit hit)
    {
        LootBag lootBag = hit.collider.GetComponentInParent<LootBag>();
        if (lootBag != null)
        {
            lootBag.PickUp();
            RestoreHeroSelection();
            return;
        }

        ItemPick itemPick = hit.collider.GetComponentInParent<ItemPick>();
        if (itemPick == null) return;

        OnItemClicked?.Invoke(itemPick);
        itemPick.PickUp();
        RestoreHeroSelection();
    }

    private void RestoreHeroSelection()
    {
        if (PartyManager.instance.SelectChars.Count == 0 && PartyManager.instance.Members.Count > 0)
            UIManager.instance.ToggleAvatar[0].isOn = true;
    }

    private void TrySelect(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        int i = 0;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Player":
                case "Hero":
                    i = SelectCharacter(hit);
                    break;
                case "Item":
                    SelectItem(hit);
                    return;
            }
        }

        if (PartyManager.instance.SelectChars.Count == 0)
            UIManager.instance.ToggleAvatar[i].isOn = true;
    }

    private void ClearRingSelection()
    {
        foreach (Characters h in PartyManager.instance.SelectChars)
            h.ToggleRingSelection(false);
    }

    private void ClearEverything()
    {
        foreach (Toggle t in UIManager.instance.ToggleAvatar)
        {
            if (t.gameObject.activeSelf)
                t.isOn = false;
        }

        ClearRingSelection();
        PartyManager.instance.SelectChars.Clear();
    }

    private void UpdateSelectionBox(Vector2 mousePos)
    {
        if (!boxSelection.gameObject.activeInHierarchy)
            boxSelection.gameObject.SetActive(true);

        float width = mousePos.x - startPos.x;
        float height = mousePos.y - startPos.y;

        boxSelection.anchoredPosition = startPos + new Vector2(width / 2, height / 2);

        width = Mathf.Abs(width);
        height = Mathf.Abs(height);

        boxSelection.sizeDelta = new Vector2(width, height);
        oldAnchoredPos = boxSelection.anchoredPosition;
    }

    private void ReleaseSelectionBox(Vector2 mousePos)
    {
        Vector2 corner1;
        Vector2 corner2;

        boxSelection.gameObject.SetActive(false);

        corner1 = oldAnchoredPos - (boxSelection.sizeDelta / 2);
        corner2 = oldAnchoredPos + (boxSelection.sizeDelta / 2);

        foreach (Characters member in PartyManager.instance.Members)
        {
            Vector2 unitPos = cam.WorldToScreenPoint(member.transform.position);

            if ((unitPos.x > corner1.x && unitPos.x < corner2.x)
                && (unitPos.y > corner1.y && unitPos.y < corner2.y))
            {
                int i = PartyManager.instance.FindIndexFromClass(member);
                UIManager.instance.ToggleAvatar[i].isOn = true;
            }
        }
        boxSelection.sizeDelta = new Vector2(0, 0);
    }
}
