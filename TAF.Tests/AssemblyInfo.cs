using log4net.Config;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(3)]
[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
