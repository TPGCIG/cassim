using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public Image[] slots; // Assign all 9 slot Images in Inspector
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private int selectedIndex = 0;

    void Start()
    {
        UpdateSelectedSlot();
    }

    void Update()
    {
        // 1-9 number keys
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateSelectedSlot();
            }
        }

        // Mouse ScrollWheel selection
    float scroll = Input.GetAxis("Mouse ScrollWheel");

    if (scroll > 0f)
    {
        // Scroll Up
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = slots.Length - 1;
        UpdateSelectedSlot();
    }
    else if (scroll < 0f)
    {
        // Scroll Down
        selectedIndex++;
        if (selectedIndex >= slots.Length)
            selectedIndex = 0;
        UpdateSelectedSlot();
    }
    }

    void UpdateSelectedSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == selectedIndex)
            {
                slots[i].color = selectedColor;
            }
            else
            {
                slots[i].color = normalColor;
            }
        }
    }
}
