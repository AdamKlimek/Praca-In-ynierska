using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip beep;
    public AudioClip Click;
    public Sprite ClickTexture;
    private Sprite StartTexture;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<AudioSource>().PlayOneShot(beep);
        gameObject.GetComponent<Image>().sprite = ClickTexture;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = StartTexture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetButtonUp("LPM") == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Click);
        }
 
            if (gameObject.name == "NewGame")
            {
                Time.timeScale = 1;
                GameObject.Find("Hero").GetComponent<RotateCharacterController>().enabled = true;
                GameObject.Find("Hero").GetComponent<AttackController>().enabled = true;
                GameObject.Find("Hero").GetComponent<ChangeWeapon>().enabled = true;
                ZombieCounter.ZombieAmount = 0;
                SpawnEnemy.ZombieCounter = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (gameObject.name == "Exit")
           {
                Application.Quit();
            }
            else if (gameObject.name == "Instructions")
            {
                UIController.page = "Instructions";
                gameObject.GetComponent<Image>().sprite = StartTexture;
            }   
            else if (gameObject.name == "ReturnToGame")
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("Menu").SetActive(false);
                UIController.MainUI.SetActive(true);
                Time.timeScale = 1;
                GameObject.Find("Hero").GetComponent<RotateCharacterController>().enabled = true;
                GameObject.Find("Hero").GetComponent<AttackController>().enabled = true;
                GameObject.Find("Hero").GetComponent<ChangeWeapon>().enabled = true;
                gameObject.GetComponent<Image>().sprite = StartTexture;
            }
            else if (gameObject.name == "Return")
            {
                if (UIController.page == "Instructions")
                {
                    UIController.page = "main";
                }
                gameObject.GetComponent<Image>().sprite = StartTexture;
            }
    }

    void Start ()
    {
        StartTexture = gameObject.GetComponent<Image>().sprite;
    }
	
	void Update ()
    {
	
	}
}
