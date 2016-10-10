using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    //public GameObject UI;
    //public GameObject Menu;
    public static GameObject MainUI;
    static public string page = "main";
    public GameObject NewGame, Instructions, Exit, ControlInstructions, Return, Credits, author;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (page == "main")
        {
            NewGame.SetActive(true);
            Instructions.SetActive(true);
            Exit.SetActive(true);
            ControlInstructions.SetActive(false);
            Return.SetActive(false);
            Credits.SetActive(true);
            author.SetActive(false);
        }
        else if (page == "Instructions")
        {
            NewGame.SetActive(false);
            Instructions.SetActive(false);
            Exit.SetActive(false);
            ControlInstructions.SetActive(true);
            Return.SetActive(true);
            Credits.SetActive(false);
            author.SetActive(false);
        }
        else if (page == "credits")
        {
            NewGame.SetActive(false);
            Instructions.SetActive(false);
            Exit.SetActive(false);
            ControlInstructions.SetActive(false);
            Return.SetActive(true);
            Credits.SetActive(false);
            author.SetActive(true);
        }
    }
}
