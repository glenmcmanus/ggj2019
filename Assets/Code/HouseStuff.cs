using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HouseStuff : MonoBehaviour
{
    [Header("Minimums & Defines")]
    public int minWoodForFirePerDay = 24;
    public int daysUntilPassBlockedAtBeginning = 20;


    [Header("Player")]
    public float staminaRegenPerFoodForPerson = 33.3f;
    public float staminaLossPerMissingWood = 1;
    public int minFoodForPersonPerDay { get { return Mathf.RoundToInt(100f / staminaRegenPerFoodForPerson); } }

    [Header("Dog")]
    public float healthRegenPerFoodForDog = 33.3f;
    public int minFoodForDogPerDay { get { return Mathf.RoundToInt(100f / healthRegenPerFoodForDog); } }
    public int maxFoodForDogPerDay = 4;
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

    public UnityEvent DogDies;
    public UnityEvent AfterSubmit;
    public UnityEvent PassBlocked;

    public void StartHouse()
    {
        Player.instance.StopAllCoroutines();
    }

    public void Submit()
    {
        // Deal with player
        Player.instance.Stamina = NextDayStamina();

        // Deal with sled building
        Sled.instance.GiveWood(woodForSled);

        // Deal with pup
        dogDisease = DogsDiseaseNextDay();
        dogHealth = DogsHealthNextDay();

        daysUntilPassBlocked -= 1;

        if(dogHealth <= 0)
        {
            DogDies.Invoke();
        }
        else if (daysUntilPassBlocked <= 0)
        {
            PassBlocked.Invoke();
        }
        else
        {
            AfterSubmit.Invoke();
        }

    }

    public float DogsHealthNextDay()
    {
        float healthForDogTonight = foodForDog * healthRegenPerFoodForDog;
        healthForDogTonight -= (minWoodForFirePerDay - woodForFire) * healthLossPerWoodGoneForDog;

        float dogDisease = DogsDiseaseNextDay();

        healthForDogTonight -= dogDisease * damageOneDiseaseDoesToDogPerNight;

        float dogHealth = this.dogHealth;
        dogHealth += healthForDogTonight;
        dogHealth = Mathf.Min(dogHealth, 100);
        return dogHealth;
    }

    public float DogsDiseaseNextDay()
    {
        float dogDisease = this.dogDisease;
        dogDisease += diseaseGrowthPerWoodGone;
        dogDisease -= medicineForDog * diseaseOneMedicineCures;
        return dogDisease;
    }

    public float NextDayStamina()
    {
        float nextDayStamina = foodForYou * staminaRegenPerFoodForPerson;
        nextDayStamina -= (minWoodForFirePerDay - woodForFire) * staminaLossPerMissingWood;
        return nextDayStamina;
    }
}
