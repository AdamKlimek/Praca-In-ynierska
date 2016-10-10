using UnityEngine;
using System.Collections;

public class DisapearHealthBar : MonoBehaviour
{
    public GameObject HealthBar;

	void Start ()
    {
        HealthBar.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50.0f))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                HealthBar.SetActive(true);
            }
            else
            {
                HealthBar.SetActive(false);
            }
        }
    }
}
