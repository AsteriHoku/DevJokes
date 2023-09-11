using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using DevJokes.Models;
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

    public async Task<MemoryStream> GenerateJokeCard(DevJoke joke)
    {
        var jokeText = await FormatJokeText(joke);
        using Bitmap bitmap = new Bitmap(400, 164);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.Black);
            using (Font font = new Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush brush = new SolidBrush(Color.Teal))
            {
                graphics.DrawString(jokeText, font, brush, new PointF(10, 10));
            }
        }

        MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public async Task<string> FormatJokeText(DevJoke joke)
    {
        var sb = new StringBuilder();
        var counter = 1;
        var lastSpace = 0;
        //split to have words
        //append words 1 by 1 and keep count of current line length
        //if length of word + length of line > 38, append \n first
        var words = joke.question.Split(" ");

        if (words[^1] == "." || words[^1] == "?")
            words[^2] = $"{words[^2]}{words[^1]}";

        var line = new StringBuilder();

        foreach (var word in words)
        {
            // +1 for the space and 38 is length for bitMap with chosen font/size
            if (line.Length + word.Length + 1 <= 38)
            {
                if (line.Length > 0)
                    line.Append(' '); // Add space if not the first word in the line

                line.Append(word);
            }
            else
            {
                sb.AppendLine(line.ToString()); // Start a new line
                line.Clear().Append(word);
            }
        }

        sb.Append(line.ToString()); // Append any remaining content
        sb.AppendLine("\n");
        
        var pWords = joke.punchline.Split(" ");

        if (pWords[^1] == "." || pWords[^1] == "?")
            pWords[^2] = $"{pWords[^2]}{pWords[^1]}";

        var pLine = new StringBuilder();

        foreach (var word in pWords)
        {
            // +1 for the space and 38 is length for bitMap with chosen font/size
            if (pLine.Length + word.Length + 1 <= 38)
            {
                if (pLine.Length > 0)
                    pLine.Append(' '); // Add space if not the first word in the pLine

                pLine.Append(word);
            }
            else
            {
                sb.AppendLine(pLine.ToString()); // Start a new line
                pLine.Clear().Append(word);
            }
        }

        sb.Append(pLine.ToString()); // Append remaining content
        sb.AppendLine("\n\n");
        sb.Append("- devjokes.azurewebsites.net");

        return sb.ToString();
    }
}