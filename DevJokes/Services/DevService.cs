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


    public async Task<DevJoke> NonAPIDevJoke()
    {
        int rand = new Random().Next(0, DevJokes.Count);
        return DevJokes[rand];
    }

    public async Task<GeekJoke> NonAPIGeekJoke()
    {
        int rand = new Random().Next(0, GeekJokes.Count);
        return GeekJokes[rand];
    }

    public async Task<NSFWjoke> NonAPINSFWProgrammingJoke()
    {
        int rand = new Random().Next(0, NSFWProgrammingJokes.Count);
        return NSFWProgrammingJokes[rand];
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

    public async Task<MemoryStream> GenerateDevJokeCard(DevJoke joke)
    {
        var jokeQuestion = await FormatJokeText(joke.question);
        var jokePunchline = await FormatJokeText(joke.punchline);
        var jokeText = $"{jokeQuestion}{jokePunchline}\n\n\n- devjokes.azurewebsites.net";
        return await StreamFromText(jokeText);
    }

    public async Task<MemoryStream> GenerateGeekJokeCard(GeekJoke joke)
    {
        var jokePunchline = await FormatJokeText(joke.joke);
        var jokeText = $"{jokePunchline}\n\n- devjokes.azurewebsites.net";
        return await StreamFromText(jokeText);
    }

    // public async Task<MemoryStream> GenerateProgrammingJokeCard(ProgrammingJoke joke)
    // {
    //     var jokeText = await FormatJokeText();
    //     return await StreamFromText();
    // }
    //
    public async Task<MemoryStream> StreamFromText (string jokeText)
    {
        using Bitmap bitmap = new Bitmap(400, 164);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.Black);
            using (Font font = new Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.Point))
            using (Brush brush = new SolidBrush(Color.MediumAquamarine))
            {
                graphics.DrawString(jokeText, font, brush, new PointF(10, 10));
            }
        }
        MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    //todo change foreach to for loop
    public async Task<string> FormatJokeText(string jokePiece)
    {
        var len = 38; //length for bitMap with chosen font/size
        var sb = new StringBuilder();
        var words = jokePiece.Split(" ");
        var line = new StringBuilder();

        // if (words[^1] == "." || words[^1] == "?")
        //     words[^2] = $"{words[^2]}{words[^1]}";

        foreach (var word in words) {
            if (line.Length + word.Length + 1 <= len) { // +1 for the space
                if (line.Length > 0)
                    line.Append(' '); // Add space if not the first word in the line

                line.Append(word);
            } else {
                sb.AppendLine(line.ToString()); // Start a new line
                line.Clear().Append(word);
            }
            if (word.EndsWith("?") || word.EndsWith(")") || word.EndsWith("."))
                line.AppendLine("\n");
        }
        sb.Append(line.ToString()); // Append any remaining content

        return sb.ToString();
    }
    
    public List<DevJoke> DevJokes = new List<DevJoke>
    {
        new DevJoke
        {
            question = "Why don't programmers like nature?",
            punchline = "It has too many bugs."
        },
        new DevJoke
        {
            question = "Why do programmers always mix up Christmas and Halloween?",
            punchline = "Because Oct 31 == Dec 25."
        },
        new DevJoke
        {
            question = "How many programmers does it take to change a light bulb?",
            punchline = "None, that's a hardware problem."
        },
        new DevJoke
        {
            question = "Why did the programmer go broke?",
            punchline = "Because he used up all his cache."
        },
        new DevJoke
        {
            question = "What do you call a programmer from Finland?",
            punchline = "Nerdic."
        },
        new DevJoke
        {
            question = "How do you comfort a JavaScript bug?",
            punchline = "You console it."
        },
        new DevJoke
        {
            question = "Why do programmers prefer iOS development over Android development?",
            punchline = "Because on iOS, you don't have to deal with Java."
        },
        new DevJoke
        {
            question = "Why did the programmer quit his job?",
            punchline = "Because he didn't get arrays."
        },
        new DevJoke
        {
            question = "Why do programmers always mix up the toilet and the computer?",
            punchline = "Because they always get stuck in the 'void.'"
        },
        new DevJoke
        {
            question = "Why don't programmers like to go outside?",
            punchline = "The sun causes too many reflections."
        },
        new DevJoke
        {
            question = "Why don't programmers like to play hide and seek?",
            punchline = "Because good players are hard to find."
        },
        new DevJoke
        {
            question = "Why do programmers always mix up Christmas and New Year's?",
            punchline = "Because Oct 31 == Dec 25 (for the geeks)."
        },
        new DevJoke
        {
            question = "Why did the programmer bring a ladder to the bar?",
            punchline = "Because he wanted to access the high-level drinks."
        },
        new DevJoke
        {
            question = "What's a programmer's favorite snack?",
            punchline = "The 'byte'-sized one."
        },
        new DevJoke
        {
            question = "Why did the programmer break up with his calculator?",
            punchline = "Because she couldn't count on him."
        },
        new DevJoke
        {
            question = "Why do programmers always say 'Hello, World!' first?",
            punchline = "Because 'Goodbye, World!' is too permanent."
        },
        new DevJoke
        {
            question = "Why was the math book sad?",
            punchline = "Because it had too many problems."
        },
        new DevJoke
        {
            question = "What do you call a programmer from the Stone Age?",
            punchline = "A 'rock' coder."
        },
        new DevJoke
        {
            question = "Why don't programmers like to take showers?",
            punchline = "Because they don't want to wash their brains."
        }
    };

    // Murphy's Laws of Computing
    // 1. When computing, whatever happens, behave as though you meant it to happen.
    // 2. When you get to the point where you really understand your computer, it's probably obsolete.
    // 3. The first place to look for information is in the section of the manual where you least expect to find it.
    // 4. When the going gets tough, upgrade.
    // 5. For every action, there is an equal and opposite malfunction.
    // 6. To err is human.. to blame your computer for your mistakes is even more human, it is downright natural.
    // 7. He who laughs last probably made a back-up.
    // 8. If at first you do not succeed, blame your computer.
    // 9. A complex system that does not work is invariably found to have evolved from a simpler system that worked just fine.
    // 10. The number one cause of computer problems is computer solutions.
    // 11. A computer program will always do what you tell it to do, but rarely what you want to do.

    public List<GeekJoke> GeekJokes = new List<GeekJoke>
    {
        new GeekJoke
        {
            joke = "How do you organize a space party? You 'planet'!"
        },
        new GeekJoke
        {
            joke = "Why did the scarecrow win an award? Because he was outstanding in his field."
        },
        new GeekJoke
        {
            joke = "Parallel lines have so much in common. It's a shame they'll never meet."
        },
        // new GeekJoke
        // {
        //     joke = "Why do seagulls fly over the sea? Because if they flew over the bay, they'd be called bagels."
        // },
        // new GeekJoke
        // {
        //     joke = "I'm on a seafood diet. I see food, and I eat it."
        // },
        // new GeekJoke
        // {
        //     joke = "I told my wife she was drawing her eyebrows too high. She looked surprised."
        // },
        // new GeekJoke
        // {
        //     joke = "What do you call a bear with no teeth? A gummy bear."
        // },
        new GeekJoke
        {
            joke = "I'm reading a book on anti-gravity. It's impossible to put down."
        },
        new GeekJoke
        {
            joke = "I used to play piano by ear, but now I use my hands."
        },
        new GeekJoke
        {
            joke = "I don't trust stairs because they're always up to something."
        },
        new GeekJoke
        {
            joke = "I couldn't figure out how to put my seatbelt on. Then it just " +
                   "clicked!"
        },
        // new GeekJoke
        // {
        //     joke = "What do you get when you cross a snowman and a vampire? Frostbite."
        // },
        // new GeekJoke
        // {
        //     joke = "Why don't skeletons fight each other? They don't have the guts."
        // },
        new GeekJoke
        {
            joke = "Why did the bicycle fall over? Because it was two-tired."
        },
        new GeekJoke
        {
            joke = "I'm friends with all electricians. We have such good current connections."
        },
        // new GeekJoke
        // {
        //     joke = "Did you hear about the kidnapping at the playground? They woke up."
        // },
        new GeekJoke
        {
            joke = "I don't trust atoms. They make up everything."
        },
        new GeekJoke
        {
            joke = "I don't always tell dad jokes, but when I do, he laughs."
        },
        new GeekJoke
        {
            joke =
                "Why did Captain Picard install automatic doors on the Enterprise? Because he wanted to 'make it so.'"
        },
        new GeekJoke
        {
            joke = "What's Captain Kirk's favorite song? 'Rocket Man' by Elton John."
        },
        new GeekJoke
        {
            joke = "Why did Worf become a gardener? Because he wanted to prune the Klingon Empire."
        },
        new GeekJoke
        {
            joke = "What do you call a Vulcan who can play the piano? Spockchopin."
        },
        new GeekJoke
        {
            joke =
                "Why did Geordi La Forge get kicked out of the bakery? Because he kept saying 'I can't see the dough!'"
        },
        new GeekJoke
        {
            joke = "How do you communicate with a Klingon baker? You speak his 'bread language.'"
        },
        new GeekJoke
        {
            joke =
                "Why don't Starfleet officers ever tell jokes about the Borg? Because they always 'assimilate' the punchline."
        },
        new GeekJoke
        {
            joke = "Why was the Klingon chef bad at making sushi? Because he couldn't find the 'right Klingon.'"
        },
        new GeekJoke
        {
            joke = "What do you call a Starfleet officer's favorite band? The Red Shirts."
        },
        new GeekJoke
        {
            joke =
                "Why did the Ferengi open a bakery on the starship? Because he wanted to make some 'dough' while traveling through space."
        },
        new GeekJoke
        {
            joke = "How many ears does Captain Kirk have? Three: a left ear, a right ear, and a final front ear."
        },
        new GeekJoke
        {
            joke = "What's Captain Picard's favorite social network? Tea-rspace."
        },
        new GeekJoke
        {
            joke = "Why did the Starfleet officer bring a ladder to the starship? Because he wanted to climb the ranks."
        },
        new GeekJoke
        {
            joke = "How do you get a Romulan's attention? Yell 'Target shields up!'"
        },
        new GeekJoke
        {
            joke =
                "Why did Data apply for a job at the library? Because he wanted to improve his 'data' processing skills."
        },
        new GeekJoke
        {
            joke = "What's the favorite game of Klingon chefs? Worfle."
        },
        new GeekJoke
        {
            joke = "Why did the Borg bring a spoon to the battle? Because resistance was futile, but dessert wasn't."
        },
        new GeekJoke
        {
            joke = "What's the Klingon version of a love letter? A discommendation."
        },
        new GeekJoke
        {
            joke = "Why did the Starfleet officer bring a pillow to the bridge? In case he encountered a 'restistance.'"
        },
        new GeekJoke
        {
            joke = "How do you fix a computer on the Enterprise? Try turning it off and 're-Jean-loting' it."
        }
    };
    
    public List<NSFWjoke> NSFWProgrammingJokes = new List<NSFWjoke>
    {
        new NSFWjoke
        {
            joke = "she asked me to pull but I \n`git push --force` \nnow I'll have to commit"
        },
        new NSFWjoke
        {
            joke = "Girls are like Internet Domain names, the ones I like are already taken."
        },
        new NSFWjoke
        {
            joke = "Programmer. \nA person who fixed a problem that you don't know you have, in a way you don't understand."
        }
    };
}