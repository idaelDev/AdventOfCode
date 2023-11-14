using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/11
    internal class Solver_11_2015 : ISolver
    {


        // R�gle 1 : Le mot doit contenir 3 lettres cons�cutives dans l'ordre de l'alphabet
        string pattern1 = @"(?=.*abc|bcd|cde|def|efg|fgh|ghi|hij|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz)";

        // R�gle 2 : Le mot ne doit PAS contenir les lettres i, o et l
        string pattern2 = @"^(?!.*[iol])";

        // R�gle 3 : Le mot doit avoir deux fois deux lettres qui se r�p�tent sans se superposer
        string pattern3 = @"^(?=(?:.*(\w)\1.*(\w)\2)|[^a-hjkmnp-z]*(\w)\3[^a-hjkmnp-z]*(\w)\4[^a-hjkmnp-z]*)";
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string s = data;

            while (!(Regex.IsMatch(s, pattern1) && Regex.IsMatch(s, pattern2) && Regex.IsMatch(s, pattern3))) 
            {
                s = IncrementWord(s);
            }

            return s;
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string s = data;
            int foundCount = 0;
            while (!(Regex.IsMatch(s, pattern1) && Regex.IsMatch(s, pattern2) && Regex.IsMatch(s, pattern3)) && foundCount < 2)
            {
                s = IncrementWord(s);
                foundCount++;
            }

            return s;
        }

        static string IncrementWord(string word)
        {
            char[] letters = word.ToCharArray();
            int n = letters.Length;

            // Parcourir le mot de droite � gauche
            for (int i = n - 1; i >= 0; i--)
            {
                if (letters[i] < 'z')
                {
                    letters[i]++;
                    break; // On a fait l'incr�mentation, pas besoin de continuer.
                }
                else
                {
                    letters[i] = 'a'; // Remettre la lettre � 'a' et passer � la lettre pr�c�dente.
                    if (i == 0)
                    {
                        Array.Resize(ref letters, n + 1);
                        Array.Copy(letters, 0, letters, 1, n);
                        letters[0] = 'a';
                    }
                }
            }

            return new string(letters);
        }
    }
}
