using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealthController playerHealth;

    [SerializeField] private GameObject Hearth1;
    [SerializeField] private GameObject Hearth2;
    [SerializeField] private GameObject Hearth3;

    [SerializeField] private GameObject txtPerdiste;

    void Start()
    {
        Hearth1.SetActive(true);
        Hearth2.SetActive(true);
        Hearth3.SetActive(true);

        txtPerdiste.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health == 3)
        {
            Hearth1.SetActive(true);
            Hearth2.SetActive(true);
            Hearth3.SetActive(true);
        }
        else if (playerHealth.health == 2)
        {
            Hearth1.SetActive(true);
            Hearth2.SetActive(true);
            Hearth3.SetActive(false);
        }
        else if (playerHealth.health == 1)
        {
            Hearth1.SetActive(true);
            Hearth2.SetActive(false);
            Hearth3.SetActive(false);
        }
        else
        {
            Hearth1.SetActive(false);
            Hearth2.SetActive(false);
            Hearth3.SetActive(false);

            txtPerdiste.SetActive(true);
        }
    }
}
