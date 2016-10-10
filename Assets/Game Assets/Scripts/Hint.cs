using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public static Image prompt;
    public  Text Dialog;
    private float TimeToShade;
    private bool Shade = false;
    public static bool IsSended = false;
    //public static Text Hints;
	void Start ()
    {
        prompt = Dialog.gameObject.transform.parent.GetComponent<Image>();
        prompt.enabled = false;
        Dialog.enabled = false;
    }
	
	void Update ()
    {
        Fade();
    }

    void ShowHint(string hint)
    {
        if (IsSended == false)
        {
            prompt.enabled = true;
            Dialog.enabled = true;

            Dialog.text = hint;
            IsSended = true;
        }
    }

    void Fade()
    {
        if (prompt.enabled == true)
        {
            if (Shade == false)
            {
                if (Dialog.color.a <= 0.3)
                {
                    Shade = true;
                }
                else
                {
                    Color col = Dialog.color;
                    col.a -= 0.02f;
                    Dialog.color = col;
                }
            }
            else if (Shade == true)
            {
                if (Dialog.color.a >= 1)
                {
                    Shade = false;
                }
                else
                {
                    Color col = Dialog.color;
                    col.a += 0.02f;
                    Dialog.color = col;
                }
            }
        }
    }

    void TurnOFFHint()
    {
        if (prompt.enabled == true)
        {
            
            prompt.enabled = false;
            Dialog.enabled = false;
            IsSended = false;
        }
    }
}
