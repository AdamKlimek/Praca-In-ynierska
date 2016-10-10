using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameObject.name == "NewGame")
        {
            SceneManager.LoadScene(1);
        }
        else if (gameObject.name == "Exit")
        {
            Application.Quit();
        }
        else if (gameObject.name == "Instructions")
        {
            Menu.page = "Instructions";
            gameObject.GetComponent<Image>().sprite = StartTexture;
        }
        else if (gameObject.name == "Credits")
        {
            Menu.page = "credits";
            gameObject.GetComponent<Image>().sprite = StartTexture;
        }
        else if (gameObject.name == "Return")
        {
            if (Menu.page == "Instructions")
            {
                Menu.page = "main";
            }
            else if (Menu.page == "credits")
            {
                Menu.page = "main";
            }
            gameObject.GetComponent<Image>().sprite = StartTexture;
        }
    }


    void Start()
    {
        StartTexture = gameObject.GetComponent<Image>().sprite;
    }

    void Update()
    {
       
    }
}
