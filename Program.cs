using System;
using System.Collections.Generic;
using System.Linq;
namespace Nagels_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string hex1 = "A4 E6 16 76 56 C7 32 05 54 B2 04 C7 46 42 E2 E2 E2 E2 E2 E2 E3 9C 15 AF 43";
            HexDecode test = new HexDecode(hex1);
            Console.WriteLine("Hello World!");
        }
    }


    //hex in format A /4E 61 67 65 6C 73 20 55 4B 20 4C 74 64 2E 2E 2E 2E 2E 2E 2E/ 39C1 /5AF43
    //     all 4 bools/TextA                                                      /ShortA/DateTimeA
    class HexDecode
    {
        public List<char> data;
        public List<List<char>> split = new List<List<char>>();
        public List<bool> bitABCD = new List<bool>();
        
        public HexDecode(string unedited){
            string[] x = unedited.Split(' ');
            data = new List<char>();
            foreach(string i in x){
                char[] temp = i.ToCharArray();
                data.Add(temp[0]);
                data.Add(temp[1]);
            }
            Splitter();
            BoolConn();
        }

        // For splitting and appropriately formatting the input into:
        //[bitsA-D,TextA,ShortA,DateTimeA]
        public void Splitter(){
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
        public void BoolConn(){
            //convert the digit into a binary nibble
            string binarystring = String.Join(String.Empty,split[0].Select
            (c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            //append the individual bits to the ABCD Array for future use
            foreach(char x in binarystring.ToCharArray()){ bitABCD.Add(Convert.ToBoolean(Convert.ToInt32(new string(x, 1))));}
        }
        //convert hex to text
        public void TextConn(){

        }
        //convert hext to short
        public void ShortConn(){

        }
        //convert and calculate date
        //adds number of days from 01/01/1000
        public void DateConn(){

        }
    }
}
