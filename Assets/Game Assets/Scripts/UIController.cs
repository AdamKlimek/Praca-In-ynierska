using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject UI;
    public GameObject Menu;
    public static  GameObject MainUI;
    static public string page = "main";
    public GameObject NewGame, Instructions, Exit, ControlInstructions, Return , ReturnToGame;

    void Start ()
    {
        MainUI = UI;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape) == true && UI.activeInHierarchy == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Menu.SetActive(true);
            UI.SetActive(false);
            Time.timeScale = 0;
            GetComponent<RotateCharacterController>().enabled = false;
            GetComponent<AttackController>().enabled = false;
            GetComponent<ChangeWeapon>().enabled = false;
        }
        else if(Input.GetKeyUp(KeyCode.Escape) == true && Menu.activeInHierarchy == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Menu.SetActive(false);
            UI.SetActive(true);
            Time.timeScale = 1;
            GetComponent<RotateCharacterController>().enabled = true;
            GetComponent<AttackController>().enabled = true;
            GetComponent<ChangeWeapon>().enabled = true;
        }

        if (page == "main")
        {
            NewGame.SetActive(true);
            Instructions.SetActive(true);
            Exit.SetActive(true);
            ControlInstructions.SetActive(false);
            Return.SetActive(false);
            ReturnToGame.SetActive(true);
        }
        else if (page == "Instructions")
        {
            NewGame.SetActive(false);
            Instructions.SetActive(false);
            Exit.SetActive(false);
            ControlInstructions.SetActive(true);
            Return.SetActive(true);
            ReturnToGame.SetActive(false);
        }
    }
}
