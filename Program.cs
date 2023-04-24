using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Nagels_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string hex1 = "A4 E6 16 76 56 C7 32 05 54 B2 04 C7 46 42 E2 E2 E2 E2 E2 E2 E3 9C 15 AF 43";
            string hex2 = "F4 26 17 47 46 C6 52 06 F6 62 04 86 17 37 46 96 E6 77 32 02 00 42 A0 5F 48";
            string hex3 = "04 16 C6 26 57 27 42 04 56 96 E7 37 46 56 96 E2 04 26 F7 26 E2 B6 74 E6 60";
            string hex4 = "C4 16 C6 16 E2 05 47 57 26 96 E6 72 04 56 E6 96 76 D6 12 02 00 8A E5 15 DA";
            string hex5 = "94 36 86 57 26 E6 F6 27 96 C2 04 46 97 36 17 37 46 57 22 12 12 3A 35 7F 34";

            HexDecode test = new HexDecode(hex1);

            HexDecode test1 = new HexDecode(hex2);
            HexDecode test2 = new HexDecode(hex3);
            HexDecode test3 = new HexDecode(hex4);
            HexDecode test4 = new HexDecode(hex5);
            //Console.WriteLine("Hello World!");
        }
    }

    //hex in format A /4E 61 67 65 6C 73 20 55 4B 20 4C 74 64 2E 2E 2E 2E 2E 2E 2E/ 39C1 /5AF43
    //     all 4 bools/TextA                                                      /ShortA/DateTimeA
    class HexDecode
    {
        //two lists one for holding the raw data, and another 2D List to hold the split data
        private List<char> data;
        private List<List<char>> split = new List<List<char>>();
        
        public List<bool> bitABCD = new List<bool>(); // hold all Bit variables in one list
        
        public string TextA = "EMPTY";
        public short ShortA;
        public DateTime DateTimeA = new DateTime(1000, 1, 1); //instantiates as the dat 01/01/1000
        
        public HexDecode(string unedited){
            //opening formatting getting it ready for splitting
            string[] firstSplit = unedited.Split(' ');
            data = new List<char>();
            foreach(string i in firstSplit){
                char[] temp = i.ToCharArray();
                data.Add(temp[0]);
                data.Add(temp[1]);
            }

            Splitter();
            BoolConn(String.Join("", split[0]));
            TextConn(String.Join("", split[1]));
            ShortConn(String.Join("",split[2]));
            DateConn(Numconn(String.Join("",split[3])));

            PrintData();
            
        }

        // For splitting and appropriately formatting the input into:
        //[bitsA-D,TextA,ShortA,DateTimeA]
        private void Splitter(){
            //range set for the split
            int[] ranges = new int[8]{0,0,1,40,41,44,45,49};
            
            List<char> subList = new List<char>();

            //loop through every other entry in ranges
            for (int i = 0; i < ranges.Length; i= i+2){
                //break at last value
                if(ranges[i] == 49){break;}
                //slice list to within the range i and i+1
                subList = (data.Where((value, index) => index >= ranges[i] && index <= ranges[i+1]).ToList());
                //Append it to a 2 dimensional list for further work
                split.Add(subList);
            }
        }

        //convert the first digit into the four bools
        private void BoolConn(string hex){
            //convert the digit into a binary nibble
            string binarystring = String.Join(String.Empty,hex.Select
            (c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            //append the individual bits to the ABCD Array for future use
            foreach(char x in binarystring.ToCharArray()){ bitABCD.Add(Convert.ToBoolean(Convert.ToInt32(new string(x, 1))));}
        }
        //convert hex to text
        private void TextConn(string hex){
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            TextA = Encoding.ASCII.GetString(raw);
        }

        //Convert Hex into a Decimal number
        private int Numconn(string hex){
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        //Convert hext to short
        private void ShortConn(string hex){
            ShortA = Convert.ToInt16(hex, 16);
        }

        //convert and calculate date
        //adds number of days from 01/01/1000
        private void DateConn(int DayAddition){
            DateTimeA = DateTimeA.Add(new TimeSpan(DayAddition,0,0,0));
        }
        private void PrintData(){
            string[] bitParts = new string[4]{"A","B","C","D"};
            for(int i = 0; i < bitABCD.Count; i++){Console.WriteLine(String.Join("Bit",bitParts[i],": ")+ bitABCD[i].ToString());}
            Console.WriteLine("TextA: "+ TextA);
            Console.WriteLine("ShortA: "+ ShortA.ToString());
            Console.WriteLine("DateTimeA: "+ DateTimeA.ToString());
            Console.WriteLine("\n");
        }
    }
}
