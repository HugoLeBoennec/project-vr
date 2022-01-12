using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    int VelocityHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKeyDown("H");
    }
}
