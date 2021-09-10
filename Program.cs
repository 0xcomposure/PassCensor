using System.Collections.Generic;
using System.IO;
using System;
using McMaster.Extensions.CommandLineUtils;

namespace PassCensor
{
    [Command(Name = "PassCensor", Description = "Censors a list of passwords")]
    [HelpOption("-H")]
    public class Program
    {
	    static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);
    
        [Argument(0, Description = "Path to the input file")]
	    private string path { get; }

	    [Argument(1, Description = "Path to the output file")]
	    private string output { get; }

        private void OnExecute()
        {
	        if (path != null && output != null)
	        {
                string file;
                List<string> censoredPasswords = new List<string>();

                file = Path.GetFullPath(path);

                using(StreamReader sr = new StreamReader(file))
                {
                    while(!sr.EndOfStream)
                    {
                        string pass = sr.ReadLine();
                        char[] ch = pass.ToCharArray();
                        for(int i = 1; i < pass.Length - 1; i++)
                        {
                            ch[i] = '*';
                        }
                        pass = new string(ch);
                        censoredPasswords.Add(pass);
                    }
                }

                using (StreamWriter outputFile = new StreamWriter(output))
                {
                    censoredPasswords.ForEach(line => outputFile.WriteLine(line));
                }
            }
            else {
                Console.WriteLine("You need to enter the input and output files");
                Console.WriteLine("Example: PassCensor /path/to/input.txt /path/to/output.txt");
            }
        }

    }
}
