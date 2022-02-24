using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private AudioSource myVoice;
    public AudioClip[] sounds; 

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        myVoice = GetComponent<AudioSource>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            myVoice.PlayOneShot(sounds[0]);
        }
        else if(other.gameObject.CompareTag("Padle"))
        {
            myVoice.PlayOneShot(sounds[1]);
        }

        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }
}
