using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

//USING THIS AS MOST OF THE CODE https://github.com/tiago-peres/blog/blob/master/csvreader/CSVReader.cs
//What i need is, the first column is the key, and the next columns are values

namespace Reuse.CSV
{
    public static class CommaSeparatedValuesReader
    {
        private const string SplitRe = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private const string LineSplitRe = @"\r\n|\n\r|\n|\r";
        private static readonly char[] TrimChars = { '\"' };
        
        public static Dictionary<string, string[]> ReadCommaSeparatedFile(TextAsset data) 
        {
            var textKeyValues = new Dictionary<string, string[]>();

            var lines = Regex.Split (data.text, LineSplitRe);

            if(lines.Length <= 1) return textKeyValues;

            var header = Regex.Split(lines[0], SplitRe);
            
            for(var i = 1; i < lines.Length; i++) { //For all the lines

                var columns = Regex.Split(lines[i], SplitRe);
                var lineKey = columns[0]; //0 is the key
                
                if(columns.Length == 0 || lineKey == "") continue;

                var columnValues = new List<string>();

                for(var j = 1; j < header.Length && j < columns.Length; j++ ) { //For all the columns, delimited by the initial header
                    var value = columns[j];
                    value = value.TrimStart(TrimChars).TrimEnd(TrimChars).Replace("\\", "");
                    
                    columnValues.Add(value);
                }
                
                textKeyValues.Add(lineKey, columnValues.ToArray()); //Easiest way
            }
            
            return textKeyValues;
        }
    }
}
