using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBorard_Checkers
{
    class UtilityMethods
    {
        public int getBit(UInt64 bitboard, int bitLocation)
        {
            if((bitboard & (1UL << bitLocation)) != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public UInt64 setBit(UInt64 bitboard, int bitLocation)
        {
            return bitboard |= 1UL << bitLocation;
        }

        public UInt64 clearBit(UInt64 bitboard, int bitLocation)
        {
            return bitboard & ~(1UL << bitLocation);
        }

        public UInt64 toggleBit(UInt64 bitboard, int bitLocation) 
        {
            return bitboard ^= 1UL << bitLocation;
        }

        public Int64 BinaryAdd(Int64 firstNum, Int64 secondNum)
        {
            Int64 carryBit = firstNum & secondNum;
            Int64 sum = firstNum ^ secondNum;

            while(carryBit != 0)
            {
                Int64 shiftedCarryBit = carryBit << 1;
                carryBit = sum & shiftedCarryBit;
                sum ^= shiftedCarryBit;
            }
            return sum;
        }

        public int BinaryAdd(int firstNum, int secondNum)
        {
            int carryBit = firstNum & secondNum;
            int sum = firstNum ^ secondNum;

            while (carryBit != 0)
            {
                int shiftedCarryBit = carryBit << 1;
                carryBit = sum & shiftedCarryBit;
                sum ^= shiftedCarryBit;
            }
            return sum;
        }

        public short BinaryAdd(short firstNum, short secondNum)
        {
            short carryBit = (short)(firstNum & secondNum);
            short sum = (short)(firstNum ^ secondNum);

            while (carryBit != 0)
            {
                short shiftedCarryBit = (short)(carryBit << 1);
                carryBit = (short)(sum & shiftedCarryBit);
                sum ^= shiftedCarryBit;
            }
            return sum;
        }

        public byte BinaryAdd(byte firstNum, byte secondNum)
        {
            byte carryBit = (byte)(firstNum & secondNum);
            byte sum = (byte)(firstNum ^ secondNum);

            while (carryBit != 0)
            {
                byte shiftedCarryBit = (byte)(carryBit << 1);
                carryBit = (byte)(sum & shiftedCarryBit);
                sum ^= shiftedCarryBit;
            }
            return sum;
        }

        public UInt64 BinarySubtract(UInt64 firstNum, UInt64 secondNum)
        {

            while (secondNum != 0)
            {
                UInt64 b = (~firstNum & secondNum);
                firstNum ^= secondNum;
                secondNum = b << 1;
            }

            return firstNum;

        }

        public Int64 BinarySubtract(Int64 firstNum, Int64 secondNum)
        {

            while(secondNum != 0)
            {
                Int64 b = (~firstNum & secondNum);
                firstNum ^= secondNum;
                secondNum = b << 1;
            }

            return firstNum;

        }

        public int BinarySubtract(int firstNum, int secondNum)
        {

            while (secondNum != 0)
            {
                int b = (~firstNum & secondNum);
                firstNum ^= secondNum;
                secondNum = b << 1;
            }

            return firstNum;

        }

        public short BinarySubtract(short firstNum, short secondNum)
        {

            while (secondNum != 0)
            {
                short b = (short)(~firstNum & secondNum);
                firstNum ^= secondNum;
                secondNum = (short)(b << 1);
            }

            return firstNum;

        }

        public sbyte BinarySubtract(sbyte firstNum, sbyte secondNum)
        {

            while (secondNum != 0)
            {
                sbyte b = (sbyte)(~firstNum & secondNum);
                firstNum ^= secondNum;
                secondNum = (sbyte)(b << 1);
            }

            return firstNum;

        }

        public UInt64 BinaryDivision(UInt64 firstNum, UInt64 secondNum)
        {
            UInt64 sign;

            firstNum = (UInt64)MathF.Abs(firstNum);
            secondNum = (UInt64)MathF.Abs(secondNum);

            UInt64 result = 0;

            while (firstNum >= secondNum)
            {
                firstNum = BinarySubtract(firstNum, secondNum);
                result++;
            }
            return result;
        }

        public Int64 BinaryDivision(Int64 firstNum, Int64 secondNum)
        {
            Int64 sign;

            if(firstNum > 0 && secondNum < 0 || firstNum < 0 && secondNum > 0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            firstNum = (Int64)MathF.Abs(firstNum);
            secondNum = (Int64)MathF.Abs(secondNum);

            Int64 result = 0;

            while(firstNum >= secondNum)
            {
                firstNum = BinarySubtract(firstNum, secondNum);
                result++;
            }
            return result * sign;
        }

        public int BinaryDivision(int firstNum, int secondNum)
        {
            int sign;

            if (firstNum > 0 && secondNum < 0 || firstNum < 0 && secondNum > 0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            firstNum = (int)MathF.Abs(firstNum);
            secondNum = (int)MathF.Abs(secondNum);

            int result = 0;

            while (firstNum >= secondNum)
            {
                firstNum = BinarySubtract(firstNum, secondNum);
                result++;
            }
            return result * sign;
        }

        public short BinaryDivision(short firstNum, short secondNum)
        {
            short sign;

            if (firstNum > 0 && secondNum < 0 || firstNum < 0 && secondNum > 0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            firstNum = (short)MathF.Abs(firstNum);
            secondNum = (short)MathF.Abs(secondNum);

            short result = 0;

            while (firstNum >= secondNum)
            {
                firstNum = BinarySubtract(firstNum, secondNum);
                result++;
            }
            return (short)(result * sign);
        }

        public sbyte BinaryDivision(sbyte firstNum, sbyte secondNum)
        {
            sbyte sign;

            if (firstNum > 0 && secondNum < 0 || firstNum < 0 && secondNum > 0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            firstNum = (sbyte)MathF.Abs(firstNum);
            secondNum = (sbyte)MathF.Abs(secondNum);

            Int64 result = 0;

            while (firstNum >= secondNum)
            {
                firstNum = BinarySubtract(firstNum, secondNum);
                result++;
            }
            return (sbyte)(result * sign);
        }

        public Int64 BinaryMultiplication(Int64 firstNum, Int64 secondNum)
        {
            Int64 result = 0;

            while (secondNum > 0)
            {
                if((secondNum & 1L) == 1)
                {
                    result += firstNum;
                }

                firstNum <<= 1;
                secondNum >>= 1;
            }

            return result;
        }

        public int BinaryMultiplication(int firstNum, int secondNum)
        {
            int result = 0;

            while (secondNum > 0)
            {
                if ((secondNum & 1L) == 1)
                {
                    result += firstNum;
                }

                firstNum <<= 1;
                secondNum >>= 1;
            }

            return result;
        }

        public short BinaryMultiplication(short firstNum, short secondNum)
        {
            short result = 0;

            while (secondNum > 0)
            {
                if ((secondNum & 1L) == 1)
                {
                    result += firstNum;
                }

                firstNum <<= 1;
                secondNum >>= 1;
            }

            return result;
        }

        public sbyte BinaryMultiplication(sbyte firstNum, sbyte secondNum)
        {
            sbyte result = 0;

            while (secondNum > 0)
            {
                if ((secondNum & 1L) == 1)
                {
                    result += firstNum;
                }

                firstNum <<= 1;
                secondNum >>= 1;
            }

            return result;
        }

        public string convertToHex(UInt64 deci)
        {
            if(deci == 0)
            {
                return "0";
            }
            string lets = "";
            while(deci != 0)
            {
                UInt64 remainder = 0;

                remainder = deci % 16;

                if(remainder > 9)
                {
                    switch (remainder)
                    {
                        case 10:
                            lets += "A";
                            break;

                        case 11:
                            lets += "B";
                            break;

                        case 12:
                            lets += "C";
                            break;

                        case 13:
                            lets += "D";
                            break;

                        case 14:
                            lets += "E";
                            break;

                        case 15:
                            lets += "F";
                            break;
                    }
                    
                }
                else
                {
                    lets += remainder;
                }

                deci /= 16;


            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToHex(uint deci)
        {
            if (deci == 0)
            {
                return "0";
            }
            string lets = "";
            while (deci != 0)
            {
                uint remainder = 0;

                remainder = deci % 16;

                if (remainder > 9)
                {
                    switch (deci % 16)
                    {
                        case 10:
                            lets += "A";
                            break;

                        case 11:
                            lets += "B";
                            break;

                        case 12:
                            lets += "C";
                            break;

                        case 13:
                            lets += "D";
                            break;

                        case 14:
                            lets += "E";
                            break;

                        case 15:
                            lets += "F";
                            break;
                    }

                }
                else
                {
                    lets += remainder;
                }

                deci /= 16;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToHex(ushort deci)
        {
            if (deci == 0)
            {
                return "0";
            }
            string lets = "";
            while (deci != 0)
            {
                ushort remainder = 0;

                remainder = (ushort)(deci % 16);

                if (remainder > 9)
                {
                    switch (deci % 16)
                    {
                        case 10:
                            lets += "A";
                            break;

                        case 11:
                            lets += "B";
                            break;

                        case 12:
                            lets += "C";
                            break;

                        case 13:
                            lets += "D";
                            break;

                        case 14:
                            lets += "E";
                            break;

                        case 15:
                            lets += "F";
                            break;
                    }

                }
                else
                {
                    lets += remainder;
                }

                deci /= 16;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToHex(byte deci)
        {
            if (deci == 0)
            {
                return "0";
            }
            string lets = "";
            while (deci != 0)
            {
                byte remainder = 0;

                remainder = (byte)(deci % 16);

                if (remainder > 9)
                {
                    switch (deci % 16)
                    {
                        case 10:
                            lets += "A";
                            break;

                        case 11:
                            lets += "B";
                            break;

                        case 12:
                            lets += "C";
                            break;

                        case 13:
                            lets += "D";
                            break;

                        case 14:
                            lets += "E";
                            break;

                        case 15:
                            lets += "F";
                            break;
                    }

                }
                else
                {
                    lets += remainder;
                }

                deci /= 16;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToHex(string binary)
        {
            
            string result = "";
            if(binary.Length % 4 != 0)
            {
                binary = binary.PadLeft(((binary.Length / 4) + 1) * 4, '0');
            }

            for(int i = 0; i < binary.Length; i += 4)
            {
                string bits = binary.Substring(i, 4);
                char[] c = bits.ToCharArray();
                Array.Reverse(c);
                bits = new string(c);

                int hexnum = 0;

                for(int j = 0; j < bits.Length; j++)
                {
                    if (bits[j] == '1')
                    {
                        hexnum += (int)MathF.Pow(2, j);
                    }
                }

                if(hexnum > 9)
                {
                    switch (hexnum)
                    {
                        case 10:
                            result += "A";
                            break;

                        case 11:
                            result += "B";
                            break;

                        case 12:
                            result += "C";
                            break;

                        case 13:
                            result += "D";
                            break;

                        case 14:
                            result += "E";
                            break;

                        case 15:
                            result += "F";
                            break;
                    }
                    
                }
                else
                {
                    result += hexnum.ToString();
                }
                
            }
            return result;
        }

        public string convertToBinary(UInt64 deci)
        {
            string lets = "";
            while (deci != 0)
            {
                lets += deci % 2;
                deci /= 2;
            }
            if(lets.Length < 64)
            {
                while(lets.Length < 64)
                {
                    lets += "0";
                }    
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToBinary(uint deci)
        {
            string lets = "";
            while (deci != 0)
            {
                lets += deci % 2;
                deci /= 2;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToBinary(ushort deci)
        {
            string lets = "";
            while (deci != 0)
            {
                lets += deci % 2;
                deci /= 2;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToBinary(byte deci)
        {
            string lets = "";
            while (deci != 0)
            {
                lets += deci % 2;
                deci /= 2;
            }
            char[] c = lets.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public string convertToBinary(string hex)
        {
            char[] c = hex.ToCharArray();
            string bin = "";

            for(int i = 0; i < c.Length; i++)
            {
                switch(c[i]) 
                {
                    case '0':
                        bin += "0000";
                        break;

                    case '1':
                        bin += "0001";
                        break;

                    case '2':
                        bin += "0010";
                        break;

                    case '3':
                        bin += "0011";
                        break;

                    case '4':
                        bin += "0100";
                        break;

                    case '5':
                        bin += "0101";
                        break;

                    case '6':
                        bin += "0110";
                        break;

                    case '7':
                        bin += "0111";
                        break;

                    case '8':
                        bin += "1000";
                        break;

                    case '9':
                        bin += "1001";
                        break;

                    case 'A':
                        bin += "1010";
                        break;

                    case 'B':
                        bin += "1011";
                        break;

                    case 'C':
                        bin += "1100";
                        break;

                    case 'D':
                        bin += "1101";
                        break;

                    case 'E':
                        bin += "1110";
                        break;

                    case 'F':
                        bin += "1111";
                        break;
                }
            }
            return bin;
        }

        public int convertToDecimal(String hex, bool isBinary)
        {
            char[] c = hex.ToCharArray();
            Array.Reverse(c);
            int deci = 0;

            for (int i = 0; i < c.Length; i++)
            {
                if (!isBinary)
                {
                    if (c[i] == 'A' || c[i] == 'B' || c[i] == 'C' || c[i] == 'D' || c[i] == 'E' || c[i] == 'F')
                    {
                        switch (c[i])
                        {
                            case 'A':
                                deci += (int)(10 * MathF.Pow(16, i));
                                break;

                            case 'B':
                                deci += (int)(11 * MathF.Pow(16, i));
                                break;

                            case 'C':
                                deci += (int)(12 * MathF.Pow(16, i));
                                break;

                            case 'D':
                                deci += (int)(13 * MathF.Pow(16, i));
                                break;

                            case 'E':
                                deci += (int)(14 * MathF.Pow(16, i));
                                break;

                            case 'F':
                                deci += (int)(15 * MathF.Pow(16, i));
                                break;
                        }
                    }
                    else
                    {
                        deci += (int)(int.Parse(c[i].ToString()) * MathF.Pow(16, i));
                    }
                }
                else
                {
                    if (c[i] == '1')
                    {
                        deci += (int)(int.Parse(c[i].ToString()) * MathF.Pow(2, i));
                    }
                }
                
            }
            return deci;
            
            
        }

        public int countNumPieces(UInt64 pieces, UInt64 kings)
        {
            int piecesNum = 0;
            for (int i = 0; i < 64; i++)
            {
                if (getBit(pieces, i) != 0 || getBit(kings, i) != 0)
                {
                    piecesNum++;
                }
            }
            return piecesNum;
        }

    }
}
