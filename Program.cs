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
                        string cred = sr.ReadLine();
                        string user = string.Empty;
                        string pass = string.Empty;

                        if (cred.Contains(':'))
                        {
                            user = cred.Substring(0, cred.IndexOf(":", StringComparison.Ordinal));
                            pass = cred.Substring(cred.IndexOf(":") + 1);
                        }
                        else
                        {
                            pass = cred;
                        }

                        char[] ch = pass.ToCharArray();
                        for(int i = 1; i < pass.Length - 1; i++)
                        {
                            ch[i] = '*';
                        }
                        pass = new string(ch);
                        
                        if (String.IsNullOrEmpty(user))
                        {
                            censoredPasswords.Add(pass);
                        }
                        else
                        {
                            censoredPasswords.Add(user + ":" + pass);
                        }
                    }
                }

                using (StreamWriter outputFile = new StreamWriter(output))
                {
                    censoredPasswords.ForEach(line => outputFile.WriteLine(line));
                    Console.WriteLine("Output saved at " + output);
                }
            }
            else {
                Console.WriteLine("You need to enter the input and output files");
                Console.WriteLine("Example: PassCensor /path/to/input.txt /path/to/output.txt");
            }
        }

    }
}
