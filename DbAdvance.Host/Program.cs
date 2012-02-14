﻿using DbAdvance.Host.Archiver;

namespace DbAdvance.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new Logger();

            if (args.Length >= 2)
            {
                var command = args[0];
                var packagePath = args[1];
                var version = args[2];
                var connectionString = args[3];

                var engine = GetEngine(logger);

                switch (command)
                {
                    case "-c":
                        engine.CommitFromVersion(packagePath, version, connectionString);
                        break;

                    case "-r":
                        engine.RollbackToVersion(packagePath, version, connectionString);
                        break;

                    default:
                        logger.Log("Unknown command: {0}", command);
                        PrintUsage(logger);
                        break;
                }
            }
            else
            {
                PrintUsage(logger);
            }
        }

        private static Engine GetEngine(ILogger logger)
        {
            return new Engine(
                logger, 
                new ZipArchiver(), 
                new FileSystem(logger), 
                new DatabaseScriptsExecutor(logger));
        }

        private static void PrintUsage(ILogger logger)
        {
            logger.Log("Db Advance:");
            logger.Log("assembly and class name.");
        }
    }
}