using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseUI : MonoBehaviour
{
    public HouseStuff houseStuff;

    public TextMeshProUGUI currentSledWoodCount;
    public TextMeshProUGUI fireWoodCount;
    public TextMeshProUGUI woodCount;

    public TextMeshProUGUI foodForDog;
    public TextMeshProUGUI foodCount;
    public TextMeshProUGUI foodForYou;

    public TextMeshProUGUI medicineCount;
    public TextMeshProUGUI medicineForDog;

    public Button toFire;
    public Button fromFire;
    public Button toSled;
    public Button fromSled;
    public Button toDogFood;
    public Button fromDogFood;
    public Button toHumanFood;
    public Button fromHumanFood;
    public Button toDogMedicine;
    public Button fromDogMedicine;

    public Image dogHealthBar;
    public Image dogDiseaseBar;
    public Image sledCompletionBar;
    public Image staminaBar;

    // Wood
    public bool toSledEnabled { get { return Player.instance.inventory.wood > 0 && Sled.instance.totalWood < Sled.instance.totalWoodNeeded && Sled.instance.totalWood + houseStuff.woodForSled < Sled.instance.totalWoodNeeded; ; } }
    public bool toFireEnabled { get { return Player.instance.inventory.wood > 0 && houseStuff.minWoodForFirePerDay > houseStuff.woodForFire; } }
    public bool fromSledEnabled { get { return houseStuff.woodForSled > 0; } }
    public bool fromFireEnabled { get { return houseStuff.woodForFire > 0; } }

    // Food
    public bool foodToPersonEnabled { get { return Player.instance.inventory.food > 0 && houseStuff.foodForYou < houseStuff.minFoodForPersonPerDay; } }
    public bool foodToDogEnabled { get { return Player.instance.inventory.food > 0 && houseStuff.foodForDog < houseStuff.minFoodForDogPerDay; } }
    public bool foodFromPersonEnabled { get { return houseStuff.foodForYou > 0; } }
    public bool foodFromDogEnabled { get { return houseStuff.foodForDog > 0; } }

    // Medicine
    public bool medicineToDogEnabled { get { return Player.instance.inventory.medicine > 0 && 
                houseStuff.medicineForDog < houseStuff.maxFoodForDogPerDay; } }

    public bool medicineFromDogEnabled { get { return houseStuff.medicineForDog > 0; } }

    // Update is called once per frame
    void Update()
    {
        toFire.interactable = toFireEnabled;
        fromFire.interactable = fromFireEnabled;
        toSled.interactable = toSledEnabled;
        fromSled.interactable = fromSledEnabled;

        toDogFood.interactable = foodToDogEnabled;
        fromDogFood.interactable = foodFromDogEnabled;
        toHumanFood.interactable = foodToPersonEnabled;
        fromHumanFood.interactable = foodFromPersonEnabled;

        toDogMedicine.interactable = medicineToDogEnabled;
        fromDogMedicine.interactable = medicineFromDogEnabled;

        currentSledWoodCount.text = houseStuff.woodForSled.ToString();
        woodCount.text = Player.instance.inventory.wood.ToString();
        fireWoodCount.text = houseStuff.woodForFire.ToString();

        foodForDog.text = houseStuff.foodForDog.ToString();
        foodForYou.text = houseStuff.foodForYou.ToString();
        foodCount.text = Player.instance.inventory.food.ToString();

        medicineCount.text = Player.instance.inventory.medicine.ToString();
        medicineForDog.text = houseStuff.medicineForDog.ToString();

        Vector3 scale;
        scale = dogHealthBar.transform.localScale;
        scale.x = Mathf.Max(houseStuff.DogsHealthNextDay() / 100f, 0);
        dogHealthBar.transform.localScale = scale;

        scale = dogDiseaseBar.transform.localScale;
        scale.x = Mathf.Max(houseStuff.DogsDiseaseNextDay() / 100f, 0);
        dogDiseaseBar.transform.localScale = scale;

        scale = sledCompletionBar.transform.localScale;
        scale.x = Mathf.Max((float)(houseStuff.woodForSled + Sled.instance.totalWood) / Sled.instance.totalWoodNeeded, 0);
        sledCompletionBar.transform.localScale = scale;

        scale = staminaBar.transform.localScale;
        scale.x = Mathf.Max(houseStuff.NextDayStamina() / 100f, 0);
        staminaBar.transform.localScale = scale;
    }

    public void GiveWoodToSled()
    {
        Player.instance.inventory.wood--;
        houseStuff.woodForSled++;
    }

    public void GiveWoodToFire()
    {
        Player.instance.inventory.wood--;
        houseStuff.woodForFire++;
    }

    public void TakeWoodFromSled()
    {
        Player.instance.inventory.wood++;
        houseStuff.woodForSled--;
    }

    public void TakeWoodFromFire()
    {
        Player.instance.inventory.wood++;
        houseStuff.woodForFire--;
    }


    public void GiveFoodToYou()
    {
        Player.instance.inventory.food--;
        houseStuff.foodForYou++;
    }

    public void GiveFoodToDog()
    {
        Player.instance.inventory.food--;
        houseStuff.foodForDog++;
    }

    public void TakeFoodFromYou()
    {
        Player.instance.inventory.food++;
        houseStuff.foodForYou--;
    }

    public void TakeFoodFromDog()
    {
        Player.instance.inventory.food++;
        houseStuff.foodForDog--;
    }



    public void GiveMedicineToDog()
    {
        Player.instance.inventory.medicine--;
        houseStuff.medicineForDog++;
    }

    public void TakeMedicineFromDog()
    {
        Player.instance.inventory.medicine++;
        houseStuff.medicineForDog--;
    }
}
