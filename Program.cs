using System;

namespace Nagels_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    //hex in format A /4E 61 67 65 6C 73 20 55 4B 20 4C 74 64 2E 2E 2E 2E 2E 2E 2E/ 39C1 /5AF43
    //     all 4 bools/TextA                                                      /ShortA/DateTimeA
    class HexDecode
    {
        public string data;
        public string[] split;
        public bool[] bitABCD;
        
        public HexDecode(string unedited){
            data = unedited;
            split  = new string[3];
            bitABCD = new bool[2];
        }

        // For splitting and appropriately formatting the input into:
        //[bitsA-D,TextA,ShortA,DateTimeA]
        public string[] Splitter(){

            return new string[] {"hello"};
        }

        //convert the first item in split array 
        public void BoolConn(){

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
