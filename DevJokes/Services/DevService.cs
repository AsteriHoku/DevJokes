using System.Text;
using DevJokes.ViewModels;

namespace DevJokes.Services;

public class DevService
{
    public DevService()
    {
        
    }

    public async Task<string> GetSetDevVM(string inputString)
    {
        var sb = new StringBuilder();

        byte[] binaryData = Encoding.ASCII.GetBytes(inputString);

        for (int i = 0; i < binaryData.Length; ++i)
        {
            sb.Append(Convert.ToString(binaryData[i], 2).PadLeft(8, '0'));
            sb.Append(" ");
        }

        return sb.ToString();
    }
}