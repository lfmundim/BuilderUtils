using BuilderUtils.Services;
using System;
using ITGlobal.CommandLine;
using System.Reflection;
using BuilderUtils.Handlers;

namespace DrawBlipBuilderFlow
{
    public class Program
    {
        private static IServicesHub _servicesHub = new ServicesHub();
        private static ISwitch _verbose;
        private static ISwitch _force;
        private static ISwitch _help;

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                UseCLI(args);
            }
            else
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1- Create output hub");
                Console.WriteLine("2- Insert extras in TrackEvents");
                Console.Write("Answer: ");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        _servicesHub.CreateOutputHub();
                        break;
                    case "2":
                        _servicesHub.InsertExtrasEventTrack();
                        break;
                    default:
                        break;
                }
            }
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        public static int UseCLI(string[] args)
        {
            return CLI.HandleErrors(() =>
            {
                var app = CLI.Parser();

                app.ExecutableName(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                app.FromAssembly(typeof(Program).GetTypeInfo().Assembly);
                app.HelpText("BLiP Builder Utils Command Line Interface");

                _verbose = app.Switch("v").Alias("verbose").HelpText("Enable verbose output.");
                _force = app.Switch("force").HelpText("Enable force operation.");
                _help = app.Switch("help");

                var outputHubHandler = new OutputHubHandler();
                var outputHubCommand = app.Command("output-hub");
                // For some unknown reason single dash parameters (ex -pth) are not registering as such, only as double dash (--path)
                outputHubHandler.Path = outputHubCommand.Parameter<string>("pth").Alias("path").HelpText("Full path to the .json file (extension included)");
                outputHubHandler.StateId = outputHubCommand.Parameter<string>("sd").Alias("stateid").Alias("st").HelpText("state.id of the hub-to-be box");
                outputHubHandler.Variable = outputHubCommand.Parameter<string>("rd").Alias("variable").Alias("var").HelpText("Builder variable to use on Output Condition");
                outputHubHandler.Verbose = _verbose;
                outputHubHandler.Force = _force;
                outputHubHandler.Help = _help;
                outputHubCommand.HelpText("Create a hub box connecting to every input-enabled box in the entire bot.");
                outputHubCommand.Handler(outputHubHandler.Run);

                app.HelpCommand();

                return app.Parse(args).Run();
            });
        }
    }
}
