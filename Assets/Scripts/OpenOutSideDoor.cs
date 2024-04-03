using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOutSideDoor : MonoBehaviour
{
    public static OpenOutSideDoor instance;

    private void Awake()
    {
        instance = this;
    }
    public bool openDoors;
    public Animator[] animators;
    private void OnValidate()
    {
        animators = GetComponentsInChildren<Animator>();
    }
  
    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();

    }


    public void OpenOutsideDoors()
    {

        foreach (Animator animator in animators)
        {
            animator.SetBool("Open", true);
        }

    }

    public void CloseOutSideDoors()
    {
        {
            foreach (Animator animator in animators)
                animator.SetBool("Open", false);
        }
    }
}
