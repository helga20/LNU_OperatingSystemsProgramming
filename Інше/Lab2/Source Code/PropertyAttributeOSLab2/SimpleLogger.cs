namespace PropertyAttributeOSLab2
{
    public class SimpleLogger
    {
        private readonly string _logPath;
        
        /// <summary>
        /// Default constructor which will use Log.txt in application folder as as log file.
        /// </summary>
        public SimpleLogger() : this(Path.Combine(Directory.GetCurrentDirectory(), "Log.txt")) { }

        /// <summary>
        /// Constructor which will use given logpath to write logs.
        /// </summary>
        /// <param name="logPath">Full path for log file.</param>
        public SimpleLogger(string logPath)
        {
            _logPath = logPath;
        }   
        
        /// <summary>
        /// Logs the string data with current timestamp.
        /// </summary>
        /// <param name="data">Data to be written in log file.</param>
        public void Log(string data)
        {
            File.AppendAllText(_logPath, $"{DateTime.Now} - {data}\n");
        }
    }
}
