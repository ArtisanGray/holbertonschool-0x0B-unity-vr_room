using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorAnimHandler : MonoBehaviour
{
    private Animator _dooranim;
    private bool opened = false;
    private bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        _dooranim = gameObject.GetComponent<Animator>();
    }
    private IEnumerator PauseDoorInteraction()
    {
        pause = true;
        yield return new WaitForSeconds(1);
        pause = false;
    }
    public void OpenorClose()
    {
        if (!opened && !pause)
        {
            _dooranim.SetBool("character_nearby", true);
            //_dooranim.Play("glass_door_open", 0, 0.0f);
            opened = true;

            StartCoroutine(PauseDoorInteraction());
        }
        if (opened && !pause)
        {
            _dooranim.SetBool("character_nearby", false);
            //_dooranim.Play("glass_door_close", 0, 0.0f);
            opened = false;
            StartCoroutine(PauseDoorInteraction());
        }
    }
}
