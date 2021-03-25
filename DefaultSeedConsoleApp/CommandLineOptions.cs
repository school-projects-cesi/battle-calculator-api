using CommandLine;

namespace DefaultSeedConsoleApp
{
    public class CommandLineOptions
    {
        [Option('s', "size", Required = false, HelpText = "Set number of users.")]
        public int Size { get; set; } = 200;

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; } = true;
    }
}
