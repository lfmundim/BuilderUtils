using System;

namespace BuilderUtils.Services
{
    public static class VerboseServices
    {
        public static void LogVerboseLine(bool verbose, string message)
        {
            if (verbose) Console.WriteLine(message);
        }
    }
}
