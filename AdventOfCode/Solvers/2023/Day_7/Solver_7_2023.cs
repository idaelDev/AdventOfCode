using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/7
    internal class Solver_7_2023 : ISolver
    {


        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {

            Dictionary<char, int> cardsConverter = new Dictionary<char, int>()
            {
                { '2',2 },
                { '3',3 },
                { '4',4 },
                { '5',5 },
                { '6',6 },
                { '7',7 },
                { '8',8 },
                { '9',9 },
                { 'T',10 },
                { 'J',11 },
                { 'Q',12 },
                { 'K',13 },
                { 'A',14 }
            };
            string[] lines = Tools.LineSplit(data);
            var ca = lines.Select(s => s.Split()[0].Select(c => cardsConverter[c]).ToArray()).ToList();
            var bi = lines.Select(s => int.Parse(s.Split()[1])).ToList();
            List<Hand> hand = ca.Zip(bi, (c, b) => new Hand() { cards = c, bid = b }).OrderBy(h => h, new HandComparer()).ToList();

            int result = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                result += hand[i].bid * (i + 1);
            }

            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            Dictionary<char, int> cardsConverter = new Dictionary<char, int>()
            {
                { 'J',1 },
                { '2',2 },
                { '3',3 },
                { '4',4 },
                { '5',5 },
                { '6',6 },
                { '7',7 },
                { '8',8 },
                { '9',9 },
                { 'T',10 },
                { 'Q',11 },
                { 'K',12 },
                { 'A',13 }
            };
            string[] lines = Tools.LineSplit(data);
            var ca = lines.Select(s => s.Split()[0].Select(c => cardsConverter[c]).ToArray()).ToList();
            var bi = lines.Select(s => int.Parse(s.Split()[1])).ToList();
            List<Hand> hand = ca.Zip(bi, (c, b) => new Hand() { cards = c, bid = b }).OrderBy(h => h, new HandComparerJoke()).ToList();

            int result = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                result += hand[i].bid * (i + 1);
            }

            return result.ToString();
        }

        internal struct Hand
        {
            public int[] cards;
            public int bid;
        }

        internal class HandComparer: IComparer<Hand>
        {
            public int Compare(Hand x, Hand y)
            {
                int xh = GetHandValue(x);
                int yh = GetHandValue(y);

                if(xh != yh)
                {
                    return xh - yh;
                }

                for(int i = 0; i<5; i++)
                {
                    if (x.cards[i] != y.cards[i])
                    {
                        return x.cards[i] - y.cards[i];
                    }
                }
                return 0;
            }

            private int GetHandValue(Hand h)
            {
                var gr = h.cards.GroupBy(c => c);
                if (gr.Any(g => g.Count() == 5))
                {
                    return 6;
                }
                if(gr.Any(g => g.Count() == 4))
                {
                    return 5;
                }
                if(gr.Any(g => g.Count() == 3))
                {
                    if(gr.Any(g => g.Count() == 2))
                    {
                        return 4;
                    }
                    return 3;
                }
                if(gr.Count(g =>g.Count() == 2) == 2)
                {
                    return 2;
                }
                if(gr.Any(g => g.Count() == 2))
                {
                    return 1;
                }
                return 0;
            }

        }

        internal class HandComparerJoke : IComparer<Hand>
        {
            public int Compare(Hand x, Hand y)
            {
                int xh = GetHandValue(x);
                int yh = GetHandValue(y);

                if (xh != yh)
                {
                    return xh - yh;
                }

                for (int i = 0; i < 5; i++)
                {
                    if (x.cards[i] != y.cards[i])
                    {
                        return x.cards[i] - y.cards[i];
                    }
                }
                return 0;
            }

            private int GetHandValue(Hand h)
            {
                var gr = h.cards.GroupBy(c => c);
                if (gr.Any(g => g.Count() == 5))
                {
                    return 6;
                }
                if (gr.Any(g => g.Count() == 4))
                {
                    if(h.cards.Contains(1))
                    {
                        return 6;
                    }
                    return 5;
                }
                if (gr.Any(g => g.Count() == 3))
                {
                    if (gr.Any(g => g.Count() == 2))
                    {
                        if(h.cards.Contains(1))
                        {
                            return 6;
                        }
                        return 4;
                    }
                    if(h.cards.Contains(1))
                    {
                        return 5;
                    }
                    return 3;
                }
                if (gr.Count(g => g.Count() == 2) == 2)
                {
                    if(gr.First(g => g.Count() ==1).Key == 1)
                    {
                        return 4;
                    }
                    if(gr.Where(g => g.Count() == 2).Select(g => g.Key).Contains(1))
                    {
                        return 5;
                    }
                    return 2;
                }
                if (gr.Any(g => g.Count() == 2))
                {
                    if(h.cards.Contains(1))
                    {
                        return 3;
                    }
                    return 1;
                }
                if(h.cards.Contains(1))
                {
                    return 1;
                }
                return 0;
            }

        }
    }
}
