using System.Text;

int getalInBinar = 0b1000101;
int getalHex = 0x6BE55A;


string tekst = "!!!";
byte[] tekstInBytes = Encoding.Default.GetBytes(tekst);
foreach (var item in tekstInBytes)
{
    Console.WriteLine(item);
}
Console.WriteLine(getalHex);
Console.ReadLine();