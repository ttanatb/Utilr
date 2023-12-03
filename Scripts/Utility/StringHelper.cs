using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilr.Structs;

namespace Utilr.Utility
{
    public static partial class Helper
    {
        /// <summary>
        /// Splits the line into two strings
        /// </summary>
        /// <param name="line"></param>
        /// <param name="splitter"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void SplitToTwo(this string line, char splitter, out string first, out string second)
        {
            string[] split = line.Split(splitter);
            first = split[0];
            second = split[1];
        }

        /// <summary>
        /// ToUpper but only the first char
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string line)
        {
            char first = char.ToUpper(line[0]);
            return first + line[1..];
        }

        /// <summary>
        /// ToUpper but only the first char
        /// </summary>
        /// <param name="line"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ToUpper(this string line, int index)
        {
            char upper = char.ToUpper(line[index]);
            return line[..index] + upper + line[(index + 1)..];
        }

        
        public static string UnderscoreLowerToCamelCase(this string line)
        {
            string result = ToUpperFirst(line);

            while (true)
            {
                int index = result.IndexOf('_');
                if (index == -1) break;
                
                Debug.Log(result);
                result = result.Remove(index, 1).ToUpper(index);
            }
            return result;
        }

        
        public static void AllIndicesOf(this string text, char c, List<int> indices)
        {
            int offset = 0;
            while (true)
            {
                int index = text.IndexOf(c, offset);
                if (index == -1) break;

                offset += index + 1;
                indices.Add(index);
            }
        }
        
        /// <summary>
        /// Characters that are not voiced.
        /// </summary>
        private static readonly HashSet<char> NOT_VOICED = new HashSet<char>()
        {
            ',', '.', '!', '?', ' ', '-', ';',
        };
        
        /// <summary>
        /// Checks if a character should be voiced in the game.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNotVoiced(this char c)
        {
            return NOT_VOICED.Contains(c);
        }
        
    }
}
