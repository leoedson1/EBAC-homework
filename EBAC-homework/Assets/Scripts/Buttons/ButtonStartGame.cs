using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartGame : MonoBehaviour
{
    public ParticleSystem particleSystem1;

    public void OnClick()
    {
        particleSystem1.Play();
    }
}
