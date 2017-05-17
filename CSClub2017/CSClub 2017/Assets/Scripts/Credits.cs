using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float durationTime = 5f;
    public float fadeTime = 2.5f;
    public float blankTime = 1f;

    string[] lines = new string[20];
    int maxLines = 0;
    int currentLine = 0;

    float duration;
    float fade;
    float blank;

    float red;
    float blue;
    float green;

    bool fadeIn = false;
    bool fadeOut = false;
    bool durationOn = false;
    bool blankOn = true;

    Text creditText;

    // Use this for initialization
    void Start()
    {
        creditText = this.GetComponent<Text>();
        Debug.Log("Current Dir " + Directory.GetCurrentDirectory());

        creditText.text = "";

        red = creditText.color.r;
        blue = creditText.color.b;
        green = creditText.color.g;

        creditText.color = new Vector4(red, green, blue, 0.0f);

        //LoadFile("Assets\\Data\\credits.txt");
        LoadResources();
        creditText.text = lines[0];

        fade = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TextChager();
    }

    private void TextChager()
    {
        if(blankOn)
        {
            //blank on
            if(Time.time >= blank)
            {
                creditText.text = lines[currentLine];
                currentLine++;
                if (currentLine == maxLines)
                    currentLine = 0;
                blankOn = false;
                fadeIn = true;
                fade = Time.time;
            }
        }
        if(fadeIn)
        {
            //fade in
            float alpha = (Time.time - fade) / fadeTime;
            //Debug.Log(Time.time + " " + fade + " " + fadeTime + " a = " + alpha);
            if (alpha < 0f)
                alpha = 0f;
            else if (alpha > 1f)
            {
                alpha = 1f;
                fadeIn = false;
                durationOn = true;
                duration = durationTime + Time.time;
            }
                
            creditText.color = new Vector4(red, green, blue, alpha);
        }
        if(durationOn)
        {
            if(Time.time >= duration)
            {
                durationOn = false;
                fadeOut = true;
                fade = Time.time + fadeTime;
                //Debug.Log("Time=" + Time.time + " Fade=" + fade );
            }
        }
        if(fadeOut)
        {
            float alpha = (Time.time - fade) / fadeTime;
            alpha = 1f - alpha;
            //Debug.Log("fadeOut = " + fade + " " + Time.time + " " + fadeTime + " " + ((Time.time - fade) / fadeTime)  + " a = " + alpha);
            if (alpha > 1f)
                alpha = 1f;
            else if (alpha < 0f)
            {
                alpha = 0f;
                fadeOut = false;
                blankOn = true;
                blank = Time.time + blankTime;
            }
            creditText.color = new Vector4(red, green, blue, alpha);
        }
    }

    private void LoadResources()
    {
        TextAsset txt = (TextAsset)Resources.Load("credits", typeof(TextAsset));
        string content = txt.text;
        lines = content.Split('\n');
        maxLines = lines.Length;
    }

    private bool LoadFile(string fileName)
    {
        try
        {
            string line;

            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            using (theReader)
            {

                do
                {
                    line = theReader.ReadLine();
                    lines[maxLines] = line;
                    maxLines++;
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }
}
