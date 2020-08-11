using System;
using System.IO;

namespace Deza.Stone.Utilities
{
    public class LogUtils
    {
        #region Variable(s)

        private static string _currentDirectory = null;

        private static string _dateFormat = "yyyy/MM/dd HH:mm:ss";

        private static string _debugFileName;

        private static string _errorFileName;

        #endregion

        #region Property(ies)

        public static string CurrentDirectory
        {
            get
            {
                if (_currentDirectory == null)
                {
                    _currentDirectory = Directory.GetCurrentDirectory();
                }

                return _currentDirectory;
            }
        }

        public static string DebugFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_debugFileName))
                {
                    _debugFileName = "DEBUG.log";
                }

                return _debugFileName;
            }
        }

        public static string ErrorFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_errorFileName))
                {
                    _errorFileName = "ERROR.log";
                }

                return _errorFileName;
            }
        }

        #endregion

        #region Constructor(s)

        private LogUtils()
        { }

        #endregion

        #region Method(s)

        public static void Debug(String message)
        {
            using (StreamWriter streamWriter = File.AppendText(CurrentDirectory + "\\" + DebugFileName))
            {
                streamWriter.WriteLine(DateTime.Now.ToString(_dateFormat) + "  :    " + message);

                streamWriter.Close();
            }
        }

        public static void Debug(String functionName, string message)
        {
            using (StreamWriter streamWriter = File.AppendText(CurrentDirectory + "\\" + DebugFileName))
            {
                streamWriter.WriteLine(DateTime.Now.ToString(_dateFormat) + "  :    " + functionName + "   >>> " + message);

                streamWriter.Close();
            }
        }

        public static void Error(String functionName, Exception ex)
        {
            using (StreamWriter streamWriter = File.AppendText(CurrentDirectory + "\\" + ErrorFileName))
            {
                streamWriter.WriteLine("###");

                streamWriter.WriteLine(DateTime.Now.ToString(_dateFormat) + "  :    " + functionName + "   >>> " + ex.ToString());

                streamWriter.WriteLine("###");

                streamWriter.Close();
            }
        }

        #endregion

    }
}
