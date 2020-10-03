using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public RawImage Image;
         
    public IEnumerator ToBlack(float duration)
    {
        for(float f = 0; f < duration; f += Time.deltaTime / duration)
        {
            Image.color = Color.Lerp(Color.clear, Color.black, f);
            yield return null;
        }
        Image.color = Color.black;
    }
    
    public IEnumerator ToClear(float duration)
    {
        for(float f = 0; f < duration; f += Time.deltaTime / duration)
        {
            Image.color = Color.Lerp(Color.black, Color.clear, f);
            yield return null;
        }
        Image.color = Color.clear;
    }
    
}
