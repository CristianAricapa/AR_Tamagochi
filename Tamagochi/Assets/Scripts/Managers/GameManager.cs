using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class c_PetInfo
    {
        public Text UI_Name;
        public Text UI_Level;
        public Text UI_Speed;
        public Text UI_Jump;
        public Text UI_Hungry;
        public Text UI_Clean;
        public Text UI_Life;
    }

    public bool GameMode = false;

    public List<GameObject> AllTarget;
    public PetsCreator.c_PetsProperties CurrentPet;
    public Vector3 PetInitPos, FinalPos;

    public c_PetInfo InfoPet;
    public GameObject ThrowPoint, Pet;
    public GameObject Ball, BallToInst;
    public bool ExistsBall = false;


    // Use this for initialization
    void Start()
    {
        //PetManager.PrintDataPet(PetsCreator.AllPets[0]);
        InitTarget();
    }

    private void InitTarget()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AllTarget.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region UpdateLevel L&I
        if (CurrentPet != null)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CurrentPet.Level++;
                PetManager.PrintDataPet(CurrentPet);
                PrintLevelInName();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                CurrentPet.Level--;
                if (CurrentPet.Level < 0)
                    CurrentPet.Level = 0;
                PetManager.PrintDataPet(CurrentPet);
                PrintLevelInName();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameMode = !GameMode;
            }

        }
        #endregion

        if (CurrentPet != null && Pet != null)
        {
            LookForward();
            Pet.transform.LookAt(FinalPos);
            CatchBall();
        }
    }

    public void SpamBall()
    {
        if (GameMode == true)
        {
            GameObject NewBall = Instantiate(Ball, ThrowPoint.transform.position, Quaternion.identity);
            BallToInst = NewBall;
            BallToInst.GetComponent<Rigidbody>().AddForce(ThrowPoint.transform.forward * 1500);
            ExistsBall = true;
        }
    }

    private void CatchBall()
    {
        if (ExistsBall == true)
        {
            FinalPos = BallToInst.transform.position;
            Pet.transform.position = Vector3.MoveTowards(Pet.transform.position, FinalPos, CurrentPet.SpeedMove * Time.deltaTime);
            if (Pet.transform.position == BallToInst.transform.position)
            {
                ExistsBall = false;
                Destroy(BallToInst);
            }
        }
        else
        {
            FinalPos = PetInitPos;
            Pet.transform.position = Vector3.MoveTowards(Pet.transform.position, FinalPos, CurrentPet.SpeedMove * Time.deltaTime * 2);
        }
    }

    public void FoundPet(string _Name)
    {
        PetManager.PrintDataPet(PetManager.GetPetByName(_Name));
        if (PlayerPrefs.HasKey("Info_" + _Name))
        {
            //Si se encuentra en los PlayerPrefs, leemos la pet
            CurrentPet = PetManager.ClonePet
                (
                _Name,
                PlayerPrefs.GetInt(_Name + "_Level"),

                PlayerPrefs.GetFloat(_Name + "_SpeedMove"),
                PlayerPrefs.GetFloat(_Name + "_JumpForce"),
                PlayerPrefs.GetFloat(_Name + "_TotalTimeHungry"),
                PlayerPrefs.GetFloat(_Name + "_TotalTimeClean"),

                PlayerPrefs.GetInt(_Name + "_HungryPercent"),
                PlayerPrefs.GetInt(_Name + "_HappyPercent"),
                PlayerPrefs.GetInt(_Name + "_LifePercent"),
                PlayerPrefs.GetInt(_Name + "_CleanPercent")
                );
        }
        else
            CurrentPet = PetManager.GetPetByName(_Name);


        CurrentPet.Pf_Pet = AssignObject(_Name);
        Pet = AssignObject(_Name).transform.GetChild(0).gameObject;
        PrintLevelInName();
        PetInitPos = Vector3.zero;
    }

    private GameObject AssignObject(string _Name)
    {
        for (int i = 0; i < AllTarget.Count; i++)
        {
            if (AllTarget[i].name == _Name)
                return AllTarget[i];
        }

        return null;
    }

    public void LostPet()
    {
        if (CurrentPet != null)
            PetManager.SavePet(CurrentPet);

        PetInitPos = Vector3.zero;

        PrintInfoPet(true);
    }

    private void PrintLevelInName()
    {
        if (CurrentPet.Pf_Pet != null)
        {
            CurrentPet.Pf_Pet.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = CurrentPet.Name + " Lvl " + CurrentPet.Level;
        }
        PrintInfoPet();
    }

    private void PrintInfoPet(bool IsLost = false)
    {
        if (IsLost == true)
        {
            InfoPet.UI_Name.text = "  ---   ";
            InfoPet.UI_Level.text = "  ---   ";
            InfoPet.UI_Speed.text = "  ---   ";
            InfoPet.UI_Jump.text = "  ---   ";
            InfoPet.UI_Hungry.text = "  ---   ";
            InfoPet.UI_Clean.text = "  ---   ";
            InfoPet.UI_Life.text = "  ---   ";

        }
        else
        {
            if (CurrentPet.Pf_Pet != null)
            {
                InfoPet.UI_Name.text = CurrentPet.Name;
                InfoPet.UI_Level.text = CurrentPet.Level.ToString();
                InfoPet.UI_Speed.text = CurrentPet.SpeedMove.ToString();
                InfoPet.UI_Jump.text = CurrentPet.JumpForce.ToString();
                InfoPet.UI_Hungry.text = CurrentPet.HungryPercent.ToString();
                InfoPet.UI_Clean.text = CurrentPet.CleanPercent.ToString();
                InfoPet.UI_Life.text = CurrentPet.LifePercent.ToString();
            }
        }
    }

    private void LookForward()
    {
        if (Pet.transform.position == PetInitPos)
        {
            Pet.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        CurrentPet.Pf_Pet.transform.GetChild(0).GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //private void Statistics(int _hungry, int _happy, int _life, int _clean, int _exp)
    //{
        
    //}
}
