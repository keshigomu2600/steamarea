using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultEndText : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        int countArrayResult = Result.roundResult.Count;
        text.text = "";

        //Result.roundResult‚ÍArrayList
        for (int i = 0; i < countArrayResult; i++)
        {
            text.text += "ROUND" + (i + 1) + " : ";
            TextContents(i);
            text.text += '\n';
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TextContents(int i)
    {
        RESULT roundResultType = (RESULT)Result.roundResult[i];
        if (roundResultType == RESULT.Success)
        {
            text.text += " O ";
        }
        else if (roundResultType == RESULT.False)
        {
            text.text += " X ";
        }
        else
        {
            text.text += " ";
        }
    }
}
