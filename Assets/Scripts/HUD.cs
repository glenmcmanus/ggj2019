using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image staminaBar;

    public TextMeshProUGUI wood;
    public TextMeshProUGUI medicine;
    public TextMeshProUGUI food;

    void Update()
    {
        wood.text = Player.instance.inventory.wood.ToString();   
        medicine.text = Player.instance.inventory.medicine.ToString();   
        food.text = Player.instance.inventory.food.ToString();

        Vector3 scale = staminaBar.transform.localScale;
        scale.x = Player.instance.Stamina / Player.instance.maxStamina;
        staminaBar.transform.localScale = scale;
    }
}
