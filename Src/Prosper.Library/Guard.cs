using System;
using System.IO;

namespace Prosper.Library
{
    public static class Guard
    {
        public static void NotNull(object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name, $"{name} must not be null.");
        }

        public static void NotNullOrWhiteSpace(string s, string name)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException($"{name} must not be null or white space.");
        }

        public static void FileExists(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File \"{path}\" could not be found.");
        }

        public static void GreaterOrEqual(float value, float min, string name)
        {
            if (value < min)
                throw new ArgumentException($"{name} must be greater or equal to {min}.", name);
        }
    }
}
