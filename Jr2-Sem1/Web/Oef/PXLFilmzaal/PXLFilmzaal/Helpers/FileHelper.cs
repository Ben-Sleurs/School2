namespace PXLFilmzaal.Helpers
{
    public class FileHelper
    {
        public static byte[] CreateByteArrayFromFile(string fileName)
        {
            byte[] returnValue = null;
            if (File.Exists(fileName))
            {
                //MemoryStream ms  new MemoryStream();
                //ms.Close();
                //ms.Dispose();
                //File.ReadAllBytes();  
                using(var ms = new MemoryStream())
                {
                    using (FileStream fs = File.OpenRead(fileName))
                    {
                        fs.CopyTo(ms);
                    }
                    returnValue = ms.ToArray();
                }
            }
            return returnValue;
        }
        public static string CreateBase64StringFromByteArray(byte[] byteArray)
        {
            string imageBase64Data = Convert.ToBase64String(byteArray);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            return imageDataURL;
        }


    }
}
