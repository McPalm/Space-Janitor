using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextBox;
    public Image Logo;
    public AudioSource MusicPlayer;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        yield return Display("Made by", 1f);
        // yield return Display("Team Stable Condition");
        yield return DisplayLogo(4f);
        yield return new WaitForSeconds(.5f);

        yield return Display("<size=24>Voice, Writing, and 2D Artist</size>\nSondy");
        yield return Display("<size=24>3D and Texture Artist</size>\nNoteworthy");
        yield return Display("<size=24>Voice, Audio and Music</size>\nBizarre Song");
        yield return Display("<size=24>3D and Texture Artist</size>\nGangrene");
        yield return Display("<size=24>Programmer</size>\nBit Assembly");

        yield return Display("For the Ludum Dare 47:th gamejam" +
            "\nStuck in a Loop", 3f);

        yield return Display("Thank you for Playing", 4f);

        while (MusicPlayer.isPlaying)
            yield return null;

        SceneManager.LoadScene(0);
    }

    IEnumerator DisplayLogo(float seconds)
    {
        for (float f = 0; f < 1f; f += Time.deltaTime * 2f)
        {
            Logo.color = Color.Lerp(Color.black, Color.white, f);
            yield return null;
        }
        Logo.color = Color.white;
        yield return new WaitForSeconds(seconds);
        for (float f = 0; f < 1f; f += Time.deltaTime * 2f)
        {
            Logo.color = Color.Lerp(Color.white, Color.black, f);
            yield return null;
        }
        Logo.color = Color.black;
    }

    IEnumerator Display(string text, float duration = 3.5f)
    {
        TextBox.text = text;
        yield return null;
        for(float f = 0; f < 1f; f += Time.deltaTime * 2f)
        {
            TextBox.color = Color.Lerp(Color.black, Color.white, f);
            yield return null;
        }
        TextBox.color = Color.white;
        yield return new WaitForSeconds(duration);
        for(float f = 0; f < 1f; f += Time.deltaTime * 2f)
        {

            TextBox.color = Color.Lerp(Color.white, Color.black, f);
            yield return null;
        }
        TextBox.color = Color.black;
        TextBox.text = "";
    }
    
}
