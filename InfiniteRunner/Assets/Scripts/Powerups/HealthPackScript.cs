using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{

    public Material canisterColor;
    // Start is called before the first frame update
    void Start()
    {
        canisterColor.SetColor("emissionColor", ColorDataBase.GetCurrentHeroColor());
        canisterColor.SetColor("baseColor", ColorDataBase.GetHealthPackColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
