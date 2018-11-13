
namespace Perceptron.src.math
{
    public static class IO
    {
        public static void WriteFile(string path, string data)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding(1251).GetBytes(data);
            using (System.IO.FileStream fileStream = new System.IO.FileStream(
                    path,
                    System.IO.FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        public static string ReadFile(string path)
            => new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd();
        
    }

    public struct ConfigInfo
    {
        //File = 0; Mode = 1; NumberOfIterations = 2; 
        //CreateNewFile = 3; CreateXLFile = 4;
        static public string[] Readed { get; set; }

        public void SetData(string input)
        {
            int index = 0, parametrsCounter = 0;
            for (; (index = input.IndexOf(":", index)) != -1; ++parametrsCounter, ++index) { }
            
            Readed = new string[parametrsCounter];
            int beginIndex = 0;
            int endIndex = 0;

            for (int p = 0; p < parametrsCounter; p++)
            {
                beginIndex = input.IndexOf(":", beginIndex) + 1;
                endIndex = input.IndexOf("\r", beginIndex);

                for (int i = beginIndex; i < endIndex; i++)
                {
                    Readed[p] += input[i];
                }
            }

        }

        public string String()
        {
            string toString = "";
            for (int i = 0; i < Readed.Length; i++)
            {
                toString += Readed[i] + " ";
            }
            return $"{toString}";
        }
    }

}
