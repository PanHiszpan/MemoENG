using System;
using System.Collections.Generic;
using System.Text;

namespace MemoEng
{

    public class Word
    {
        public string eng;
        public string[] pl;
        public string context;

        public Word(string[] splitLine)
        {
            eng = splitLine[0];
            pl = splitLine[1].Split(',');  //rozdzielenie roznych tlumaczen
            if (splitLine.Length > 2)
            {
                context = splitLine[2];
            }
        }

    }
}
