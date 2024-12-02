using System.Text;

namespace AdventOfCode.API
{
    internal static class Tools
    {

        /// <summary>
        /// Split text and remove empty lines
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] EmpltyLineSplit(string text)
        {
            return text.Split(@$"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Split text by lines
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] LineSplit(string text)
        {
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}