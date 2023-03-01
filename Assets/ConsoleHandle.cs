using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHandle : MonoBehaviour
{
    private GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
      particle = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ParticleToggle()
    {
        if (particle.activeSelf == true)
        {
            particle.SetActive(false);
        }
        else
            particle.SetActive(true);
    }
}
