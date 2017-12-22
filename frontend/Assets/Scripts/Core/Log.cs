using System.IO;


namespace Core {

    public class Log {

        private static StreamWriter _fileStream;


        static Log () {
            #if !UNITY_EDITOR
            _fileStream = File.CreateText(Settings.LogPath);
            #endif
        }


        public static void Print (string s) {
            #if !UNITY_EDITOR
            _fileStream.WriteLine(s);
            #endif
        }

    }

}
