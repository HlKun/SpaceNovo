using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceNovo.Models;

namespace SpaceNovo.Helper
{
    public delegate void ReadFileHandler(FileData fileData, double percent);

    public class FileReadHelper
    {
        public static void ReadFile(string filePath,ReadFileHandler readFileHandler)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int index = 0;

                var allLines = reader.ReadToEnd().Split('\n');

                for (int i = 0; i < allLines.Length; i++)
                {
                    if (string.IsNullOrEmpty(allLines[i]))
                        continue;

                    string[] strs = allLines[i].Split('\t');

                    FileData fileData = new FileData
                    {
                        Index = ++index,
                        X = Convert.ToDouble(strs[0]),
                        YS = new List<double>()
                    };

                    for (int j = 1; j < strs.Length; j++)
                    {
                        fileData.YS.Add(Convert.ToDouble(strs[j]));
                    }

                    readFileHandler.Invoke(fileData, (double)(i + 1) / allLines.Length);
                }

                readFileHandler.Invoke(null, 100);
            }
        }
    }
}
