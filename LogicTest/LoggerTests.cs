using Logic.Services;

namespace LogicTest
{
    [TestClass]
    [DoNotParallelize]
    public class LoggerTests
    {
        private static string TodayLogPath =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TPW", "Logs", $"{DateTime.Now:yyyyMMdd}.log");

        [TestInitialize]
        public void CleanLogFile()
        {
            if (File.Exists(TodayLogPath))
                File.Delete(TodayLogPath);
        }

        [TestMethod]
        public void Ctor_ShouldCreateLogFile_AndWriteDebugLine()
        {
            _ = new Logger();

            Assert.IsTrue(File.Exists(TodayLogPath), "Plik logu nie został utworzony");
            string last = File.ReadLines(TodayLogPath).Last();
            StringAssert.Contains(last, "[Debug]");
            StringAssert.Contains(last, "Logging to");
        }

        [TestMethod]
        public void Log_Warning_ShouldWriteEntry_AndRestoreConsoleColor()
        {
            var logger = new Logger();
            ConsoleColor startColor = Console.ForegroundColor;

            logger.Log("ostrzeżenie testowe", LogLevel.Warning);

            Assert.AreEqual(startColor, Console.ForegroundColor, "Kolor konsoli nie został przywrócony");

            string last = File.ReadLines(TodayLogPath).Last();
            StringAssert.Contains(last, "[Warning]");
            StringAssert.Contains(last, "ostrzeżenie testowe");
        }

        [TestMethod]
        public void Warn_ShouldWriteWarningLevel()
        {
            var logger = new Logger();

            logger.Warn("to tylko warning");

            string last = File.ReadLines(TodayLogPath).Last();
            StringAssert.Contains(last, "[Warning]");
            StringAssert.Contains(last, "to tylko warning");
        }

        [TestMethod]
        public void Error_ShouldWriteErrorLevel()
        {
            var logger = new Logger();

            logger.Error("fatalny błąd");

            string last = File.ReadLines(TodayLogPath).Last();
            StringAssert.Contains(last, "[Error]");
            StringAssert.Contains(last, "fatalny błąd");
        }

        [TestMethod]
        public void Info_ShouldWriteInfoLevel()
        {
            var logger = new Logger();

            logger.Info("zwykła informacja");

            string last = File.ReadLines(TodayLogPath).Last();
            StringAssert.Contains(last, "[Info]");
            StringAssert.Contains(last, "zwykła informacja");
        }
    }
}
