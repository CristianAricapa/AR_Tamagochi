using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsCreator : MonoBehaviour
{
    [System.Serializable]
    public class c_PetsProperties
    {
        public string Name;
        public int Level;

        public float SpeedMove;
        public float JumpForce;

        public int LifePercent = 100;
        public int HungryPercent = 100;
        public int HappyPercent = 100;
        public int CleanPercent = 100;

        public float TotalTimeHungry;
        public float TotalTimeClean;

        public GameObject Pf_Pet = null;
    }

    static public List<c_PetsProperties> AllPets = new List<c_PetsProperties>
    {
        new c_PetsProperties(){Name = "Muggy",          SpeedMove = 12,   JumpForce = 8, TotalTimeHungry = 10, TotalTimeClean = 25},
        new c_PetsProperties(){Name = "Octy",           SpeedMove = 18,   JumpForce = 5, TotalTimeHungry = 15, TotalTimeClean = 35},
        new c_PetsProperties(){Name = "EyeBall",        SpeedMove = 10,   JumpForce = 8, TotalTimeHungry = 10, TotalTimeClean = 20},
        new c_PetsProperties(){Name = "Argo",           SpeedMove = 8,    JumpForce = 3, TotalTimeHungry = 12, TotalTimeClean = 30}
    };
}
