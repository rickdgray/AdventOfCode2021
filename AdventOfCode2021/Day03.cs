using System.Text;

namespace AdventOfCode2021
{
    public class Day03
    {
        public static long Part1(List<string> data)
        {
            var placeCount = data[0].Length;
            var bitPlacesSums = new List<int>(new int[placeCount]);
            for (int i = 0; i < placeCount; i++)
            {
                foreach (var binary in data)
                {
                    if (binary[i].Equals('1'))
                    {
                        bitPlacesSums[i]++;
                    }
                }
            }

            var gammaRateList = new List<int>(new int[placeCount]);
            var epsilonRateList = new List<int>(new int[placeCount]);
            for (int i = 0; i < bitPlacesSums.Count; i++)
            {
                gammaRateList[i] = bitPlacesSums[i] > data.Count / 2 ? 1 : 0;
                epsilonRateList[i] = bitPlacesSums[i] > data.Count / 2 ? 0 : 1;
            }

            var gsb = new StringBuilder();
            foreach(var gammaRatePlace in gammaRateList)
            {
                gsb.Append(gammaRatePlace);
            }
            var gammaRate = Convert.ToInt32(gsb.ToString(), 2);

            var esb = new StringBuilder();
            foreach (var epsilonRatePlace in epsilonRateList)
            {
                esb.Append(epsilonRatePlace);
            }
            var epsilonRate = Convert.ToInt32(esb.ToString(), 2);

            return gammaRate * epsilonRate;
        }

        public static long Part2(List<string> data)
        {
            var placeCount = data[0].Length;

            var oxyList = new List<string>(data);
            for (int i = 0; i < placeCount; i++)
            {
                var onesCount = 0;
                for (int j = 0; j < oxyList.Count; j++)
                {
                    if (oxyList[j][i].Equals('1'))
                    {
                        onesCount++;
                    }
                }

                var keepBit = onesCount >= oxyList.Count / 2d ? '1' : '0';

                var oxyRemoveList = new List<string>();
                for (int j = 0; j < oxyList.Count; j++)
                {
                    if (!oxyList[j][i].Equals(keepBit))
                    {
                        oxyRemoveList.Add(oxyList[j]);
                    }
                }

                oxyList.RemoveAll(x => oxyRemoveList.Contains(x));
                oxyRemoveList = new List<string>();

                if (oxyList.Count < 2)
                {
                    break;
                }
            }

            var c02List = new List<string>(data);
            for (int i = 0; i < placeCount; i++)
            {
                var onesCount = 0;
                for (int j = 0; j < c02List.Count; j++)
                {
                    if (c02List[j][i].Equals('1'))
                    {
                        onesCount++;
                    }
                }

                var keepBit = onesCount < c02List.Count / 2d ? '1' : '0';

                var c02RemoveList = new List<string>();
                for (int j = 0; j < c02List.Count; j++)
                {
                    if (!c02List[j][i].Equals(keepBit))
                    {
                        c02RemoveList.Add(c02List[j]);
                    }
                }

                c02List.RemoveAll(x => c02RemoveList.Contains(x));
                c02RemoveList = new List<string>();

                if (c02List.Count < 2)
                {
                    break;
                }
            }

            var osb = new StringBuilder();
            foreach(var oxyPlace in oxyList[0])
            {
                osb.Append(oxyPlace);
            }
            var oxyRate = Convert.ToInt32(osb.ToString(), 2);

            var esb = new StringBuilder();
            foreach (var c02Place in c02List[0])
            {
                esb.Append(c02Place);
            }
            var c02Rate = Convert.ToInt32(esb.ToString(), 2);

            return oxyRate * c02Rate;
        }
    }
}