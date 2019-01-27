using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HouseValues")]
public class HouseStuff : ScriptableObject
{
    [Header("Minimums & Defines")]
    public int minWoodForFirePerDay = 24;
    public int daysUntilPassBlockedAtBeginning = 20;


    [Header("Player")]
    public int minFoodForPersonPerDay = 3;
    public float healthRegenPerFoodForPerson = 33.3f;
    public float staminaLossPerMissingWood = 1;

    [Header("Dog")]
    public float healthRegenPerFoodForDog = 33.3f;
    public int diseaseOneMedicineCures = 5;
    public int diseaseAdvancesPerDay = 10;
    public float healthLossPerWoodGoneForDog = 1;
    public float diseaseGrowthPerWoodGone = 2;
    public float damageOneDiseaseDoesToDogPerNight = 0.5f;

    public int minMedicinePerDayForDogPerDay { get { return diseaseAdvancesPerDay / diseaseOneMedicineCures; } }


    [Header("Runtime Variables For UI")]
    public int woodForSled = 0;
    public int woodForFire = 0;
    public int foodForDog = 0;
    public int foodForYou = 0;
    public int medicineForDog = 0;

    [Header("Runtime Variables For Doggo")]
    public float dogDisease = 100;
    public float dogHealth = 25;

    [Header("Misc")]
    public int daysUntilPassBlocked = 20;

    public void StartHouse()
    {
        Player.instance.StopAllCoroutines();
    }

    public void Submit()
    {
        // Deal with player
        Player.instance.inventory.food -= foodForYou;
        Player.instance.inventory.wood -= woodForFire;

        float nextDayStamina = foodForYou * healthRegenPerFoodForPerson;
        nextDayStamina -= (minWoodForFirePerDay - woodForFire) * staminaLossPerMissingWood;

        // Deal with sled building
        Player.instance.inventory.wood -= woodForSled;
        Sled.instance.GiveWood(woodForSled);

        // Deal with pup
        Player.instance.inventory.food -= foodForDog;
        Player.instance.inventory.medicine -= medicineForDog;

        float healthForDogTonight = foodForDog * healthRegenPerFoodForDog;
        healthForDogTonight -= (minWoodForFirePerDay - woodForFire) * healthLossPerWoodGoneForDog;

        dogDisease += diseaseGrowthPerWoodGone;
        dogDisease -= medicineForDog * diseaseOneMedicineCures;

        healthForDogTonight -= dogDisease * damageOneDiseaseDoesToDogPerNight;
        dogHealth += healthForDogTonight;
        dogHealth = Mathf.Max(dogHealth, 100);

        daysUntilPassBlocked -= 1;

        if(dogHealth <= 0)
        {
            TheDogFuckingDies();
        }
    }

    public void TheDogFuckingDies()
    {

    }
}
