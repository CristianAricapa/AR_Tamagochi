using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    static public PetsCreator.c_PetsProperties GetPetByName(string PetName)
    {
        for (int i = 0; i < PetsCreator.AllPets.Count; i++)
        {
            if (PetsCreator.AllPets[i].Name == PetName)
            {
                return PetsCreator.AllPets[i];
            }
        }

        return null;
    }

    static public PetsCreator.c_PetsProperties ClonePet(string PetName, int PetLevel, float PetSpeedMove,
                                                   float PetJumpForce, float PetTimeHungry, float PetTimeClean,
                                                   int PetHungryPercent, int PetHappyPercent, int PetLifePercent,
                                                   int PetCleanPercent)
    {
        PetsCreator.c_PetsProperties TempPet = new PetsCreator.c_PetsProperties()
        {
            Name = PetName,
            Level = PetLevel,
            SpeedMove = PetSpeedMove,
            JumpForce = PetJumpForce,
            TotalTimeHungry = PetTimeHungry,
            TotalTimeClean = PetTimeClean,
            HungryPercent = 100,
            HappyPercent = 100,
            LifePercent = 100,
            CleanPercent = 100
        };

        return TempPet;
    }

    static public void SavePet(PetsCreator.c_PetsProperties Pet)
    {
        PlayerPrefs.SetInt("Info_" + Pet.Name, 1); //Buscamos el parametro, y si existe devuelve 1
        //Si el parametro existe, cargo las siguientes lineas
        PlayerPrefs.SetInt(Pet.Name + "_Level", Pet.Level);

        PlayerPrefs.SetFloat(Pet.Name + "_SpeedMove", Pet.SpeedMove);
        PlayerPrefs.SetFloat(Pet.Name + "_JumpForce", Pet.JumpForce);

        PlayerPrefs.SetFloat(Pet.Name + "_TotalTimeClean", Pet.TotalTimeClean);
        PlayerPrefs.SetFloat(Pet.Name + "_TotalTimeHungry", Pet.TotalTimeHungry);

        PlayerPrefs.SetInt(Pet.Name + "_CleanPercent", Pet.CleanPercent);
        PlayerPrefs.SetInt(Pet.Name + "_HappyPercent", Pet.HappyPercent);
        PlayerPrefs.SetInt(Pet.Name + "_HungryPercent", Pet.HungryPercent);
        PlayerPrefs.SetInt(Pet.Name + "_LifePercent", Pet.LifePercent);
    }

    static public void PrintDataPet(PetsCreator.c_PetsProperties Pet)
    {
        string TempString = "";

        TempString += "Name: " + Pet.Name + "\n";
        TempString += "Level : " + Pet.Level + "\n";
        TempString += "Speed Move : " + Pet.SpeedMove + "\n";
        TempString += "Jump Force : " + Pet.JumpForce + "\n";

        TempString += "Total Time Clean : " + Pet.TotalTimeClean + "\n";
        TempString += "Total Time Hungry : " + Pet.TotalTimeHungry + "\n";

        TempString += "Clean Percent : " + Pet.CleanPercent + "\n";
        TempString += "Happy Percent : " + Pet.HappyPercent + "\n";
        TempString += "Hungry Percent : " + Pet.HungryPercent + "\n";
        TempString += "Life Percent : " + Pet.LifePercent + "\n";

        Debug.Log(TempString);
    }

}
