using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float timeBetweenLetters;
    public string textToType;

    private void Awake()
    {
        textMesh.text = "";
    }

    [NaughtyAttributes.Button]
    public void StartType()
    {
        StartCoroutine(Type(textToType));
    }

    IEnumerator Type(string s)
    {
        textMesh.text = "";
        foreach(char l in s.ToCharArray())
        {
            textMesh.text += l;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }
}
