using System.Runtime.CompilerServices;

namespace BitBorard_Checkers
{
    internal class Program
    {
        UtilityMethods UM = new UtilityMethods(); //A variable that allows me to call my Utility Class Methods
        int[,] boardSpaces = { 
            { 56, 57, 58, 59, 60, 61, 62, 63 }, 
            { 48, 49, 50, 51, 52, 53, 54, 55 }, 
            {40, 41, 42, 43, 44, 45, 46, 47},
            {32, 33, 34, 35, 36, 37, 38, 39}, 
            {24, 25, 26, 27, 28, 29, 30, 31}, 
            {16, 17, 18, 19, 20, 21, 22, 23}, 
            {8, 9, 10, 11, 12, 13, 14, 15}, 
            {0, 1, 2, 3, 4, 5, 6, 7} }; 
        //A 2d array that allows my move input to work as feeding in the two numbers for the start and end spaces corrispond to
        //a number that is used to get a specific position in a bitboard.


        UInt64 blackPieces;
        UInt64 blackKings;
        UInt64 redPieces;
        UInt64 redKings;
        bool showBinary = true; //Determins if binary or hexadecimal is shown next to the board
        int turnNum;
        static void Main(string[] args)
        {
            
            Program program = new Program();
            
            program.GameLogic();           
        }
        void initilizeBoards(bool isBlackPlayer)
        {
            blackPieces = 5614165;
            blackKings = 0;
            redPieces = 12273903276444876800;
            redKings = 0;
            turnNum = 1;
            boardShower(isBlackPlayer, true);
        }

        void boardShower(bool isBlackPlayer, bool showBinary)
        {
            if(isBlackPlayer)//Prints the turn number and whose turn it is at the top of the board.
            {
                Console.WriteLine("   Turn Number:" + turnNum + "-Black's Turn");
            }
            else
            {
                Console.WriteLine("   Turn Number:" + turnNum + "-Red's Turn");
            }
            
            for(int r = 0; r < 8; r++)
            {
                for(int f = 0; f < 8; f++) 
                {
                    int sq = r * 8 + f;

                    if(f == 0)
                    {
                        Console.Write(" " + (8-r) + "| ");
                    }
                   
                    if(UM.getBit(blackPieces, sq) == 1)
                    {
                        Console.Write(" b ");
                    }
                    else if(UM.getBit(blackKings, sq) == 1)
                    {
                        Console.Write(" B ");
                    }
                    else if(UM.getBit(redPieces, sq) == 1)
                    {
                        Console.Write(" r ");
                    }
                    else if (UM.getBit(redKings, sq) == 1)
                    {
                        Console.Write(" R ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }

                    
                }
                Console.Write(" |");
                switch (r)
                {
                    case 0:
                        Console.Write("Number of Pieces In Play:");
                        break;

                    case 1:
                        Console.Write("Red Pieces: " + UM.countNumPieces(redPieces, redKings));
                        break;

                    case 2:
                        Console.Write("Black Pieces: " + UM.countNumPieces(blackPieces, blackKings));
                        break;

                    case 3:
                        if(showBinary)
                        {
                            Console.Write("Binary Bitboards:");
                        }
                        else
                        {
                            Console.Write("Hex Bitboards:");
                        }
                        break;

                    case 4:
                        if (showBinary)
                        {
                            Console.Write("Red Pieces: " + UM.convertToBinary(redPieces));
                        }
                        else
                        {
                            Console.Write("Red Pieces: " + UM.convertToHex(redPieces));
                        }
                        break;

                    case 5:
                        if (showBinary)
                        {
                            Console.Write("Red Kings: " + UM.convertToBinary(redKings));
                        }
                        else
                        {
                            Console.Write("Red Kings: " + UM.convertToHex(redKings));
                        }
                        break;

                    case 6:
                        if (showBinary)
                        {
                            Console.Write("Black Pieces: " + UM.convertToBinary(blackPieces));
                        }
                        else
                        {
                            Console.Write("Black Pieces: " + UM.convertToHex(blackPieces));
                        }
                        break;

                    case 7:
                        if (showBinary)
                        {
                            Console.Write("Black Kings: " + UM.convertToBinary(blackKings));
                        }
                        else
                        {
                            Console.Write("Black Kings: " + UM.convertToHex(blackKings));
                        }
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine("   __________________________");
            Console.WriteLine("\n     1  2  3  4  5  6  7  8\n\n");
        }

        public void GameLogic()
        {
            bool gameHasEnded = false;
            bool isBlackPlayer = false;
            initilizeBoards(isBlackPlayer);

            while(!gameHasEnded)
            {
                //The basic game loop. Starts with the red player moving a piece, then swaping to the black player. The if-else
                //at the bottom checks to see if someone has won by removing all of their enemies pieces.
                movePiece(isBlackPlayer);
                isBlackPlayer = !isBlackPlayer;
                turnNum++;
                boardShower(isBlackPlayer, showBinary);

                if(UM.countNumPieces(blackPieces, blackKings) == 0)
                {
                    gameHasEnded = true;
                    Console.WriteLine("Red Has Won");
                }
                else if(UM.countNumPieces(redPieces, redKings) == 0)
                {
                    gameHasEnded = true;
                    Console.WriteLine("Black Has Won");
                }
            }
        }

        public void movePiece(bool isBlackPlayer)
        {
            //The main method that handles piece movement. It creates a class variable of the MoveInfo class to help store
            //information about a move to make the movement of a piece a little more clear. The while loop will go on until
            //a valid move is made with that validation being handled by the ValidateMove method which is called in the while
            //loop.
            bool validMove = false;
            int[] inputs;
            string inputLine;
            MoveInfo MI = new MoveInfo();

            while(!validMove)
            {
                Console.WriteLine("Please enter your move as: start row, start column,end row, end column");
                Console.WriteLine("I.e, 1,1,2,2");
                Console.WriteLine("Also Type: 'Hex' to show hexadecimal values and 'Binary' to show binary values");

                inputLine = Console.ReadLine();

                //An if, else-if, else statement that allows a player to either change the values showed next to the board
                //to bianry or hexadeciaml or input a move.
                if(inputLine == "Hex")
                {
                    showBinary = false;
                    boardShower(isBlackPlayer, showBinary);
                }
                else if(inputLine == "Binary")
                {
                    showBinary= true;
                    boardShower(isBlackPlayer, showBinary);
                }
                else
                {
                    //Handles player imput which is supposed to come in the format of something like 1,3,5,4
                    //This is startSquare row, startSquare column, endSquare row, endSquare column.
                    //The inputs are then put in the for loop to decrease them by one in orde to be used in the 2d array at
                    //the very top.
                    inputs = inputLine.Split(',').Select(int.Parse).ToArray();
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        inputs[i] -= 1;
                    }

                    //The call to the validateMove method that returns a MoveInfo variable;
                    MI = validateMove(inputs[0], inputs[1], inputs[2], inputs[3], isBlackPlayer, MI);

                    //The big main if else statement that actually handles the moving of pieces. If a move was a valid one
                    // then the following if statements will take information about the move from the variable MI such as
                    //if the moved piece was a king, if a piece is being promoted or if the piece was making a jump. With this
                    //a move can accuretly be made and the bitboards updated to represent the move.
                    if (MI.isValidMove)
                    {
                        if (isBlackPlayer)
                        {
                            if (!MI.isMovedPieceKing)
                            {
                                blackPieces = UM.setBit(blackPieces, boardSpaces[inputs[2], inputs[3]]);
                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0], inputs[1]]);
                            }
                            else
                            {
                                blackKings = UM.setBit(blackKings, boardSpaces[inputs[2], inputs[3]]);
                                blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0], inputs[1]]);
                            }

                            if (MI.isJumping)
                            {
                                if (inputs[1] > inputs[3])
                                {
                                    if(MI.isMovedPieceKing)
                                    {
                                        if (inputs[0] > inputs[2])
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                redKings = UM.clearBit(redKings, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                            }
                                            else
                                            {
                                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                            }
                                        }
                                        else
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                redKings = UM.clearBit(redKings, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                            }
                                            else
                                            {
                                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (MI.isEnemyPieceKing)
                                        {
                                            redKings = UM.clearBit(redKings, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                        }
                                        else
                                        {
                                            redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    if (MI.isMovedPieceKing)
                                    {
                                        if (inputs[0] > inputs[2])
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                redKings = UM.clearBit(redKings, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                            }
                                            else
                                            {
                                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                            }
                                        }
                                        else
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                redKings = UM.clearBit(redKings, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                            }
                                            else
                                            {
                                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (MI.isEnemyPieceKing)
                                        {
                                            redKings = UM.clearBit(redKings, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                        }
                                        else
                                        {
                                            redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                        }
                                    }
                                }
                                
                            }

                        }
                        else
                        {
                            if (!MI.isMovedPieceKing)
                            {
                                redPieces = UM.setBit(redPieces, boardSpaces[inputs[2], inputs[3]]);
                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[0], inputs[1]]);
                            }
                            else
                            {
                                redKings = UM.setBit(redKings, boardSpaces[inputs[2], inputs[3]]);
                                redKings = UM.clearBit(redKings, boardSpaces[inputs[0], inputs[1]]);
                            }

                            if (MI.isJumping)
                            {
                                if (inputs[1] > inputs[3])
                                {
                                    if (MI.isMovedPieceKing)
                                    {
                                        if (inputs[0] > inputs[2])
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                            }
                                            else
                                            {
                                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] - 1, inputs[1] - 1]);
                                            }
                                        }
                                        else
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                            }
                                            else
                                            {
                                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MI.isEnemyPieceKing)
                                        {
                                            blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                        }
                                        else
                                        {
                                            blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] + 1, inputs[1] - 1]);
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    if (MI.isMovedPieceKing)
                                    {
                                        if (inputs[0] > inputs[2])
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                            }
                                            else
                                            {
                                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] - 1, inputs[1] + 1]);
                                            }
                                        }
                                        else
                                        {
                                            if (MI.isEnemyPieceKing)
                                            {
                                                blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                            }
                                            else
                                            {
                                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MI.isEnemyPieceKing)
                                        {
                                            blackKings = UM.clearBit(blackKings, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                        }
                                        else
                                        {
                                            blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[0] + 1, inputs[1] + 1]);
                                        }
                                    }
                                    
                                }

                                
                            }
                        }

                        if (MI.isPromotionMove)
                        {
                            if (isBlackPlayer)
                            {
                                blackPieces = UM.clearBit(blackPieces, boardSpaces[inputs[2], inputs[3]]);
                                blackKings = UM.setBit(blackKings, boardSpaces[inputs[2], inputs[3]]);
                            }
                            else
                            {
                                redPieces = UM.clearBit(redPieces, boardSpaces[inputs[2], inputs[3]]);
                                redKings = UM.setBit(redKings, boardSpaces[inputs[2], inputs[3]]);
                            }
                        }

                        validMove = true;
                    }
                    else
                    {
                        Console.WriteLine("That was an invalid move. Please try again");
                    }
                }

                
            }
        }

        public MoveInfo validateMove(int sr, int sc, int er, int ec, bool isBlackPlayer, MoveInfo MI)
        {
            //The big metod that is used to validate whether or not a move is legal. This is done by taking in information
            //about the move, such as the row and column of the starting and end square, and running that through various if
            //statements that represent various rules of checkers.
            MI.isValidMove = false;

            //Rule Check For If The Starting Square Has A Piece. This is done by using the getBit method from the utiltiy class
            //to see if the bit at the start square position the player designated is equal to 1. If it is, it also checks to
            //see if the piece is a king or a normal piece and stores that in the MI variable.
            if (isBlackPlayer)
            {
                if (UM.getBit(blackPieces, boardSpaces[sr, sc]) != 1 && UM.getBit(blackKings, boardSpaces[sr, sc]) != 1)
                {
                    Console.WriteLine("Invalid Move: There is No Black Piece at Start Row and Column");
                    return MI;
                }
                else
                {
                    //Checks if the select Piece is a normal one or a king.
                    if (UM.getBit(blackPieces, boardSpaces[sr, sc]) == 1)
                    {
                        MI.isMovedPieceKing = false;
                    }
                    else if (UM.getBit(blackKings, boardSpaces[sr, sc]) == 1)
                    {
                        MI.isMovedPieceKing = true;
                    }
                }
            }
            else
            {
                if (UM.getBit(redPieces, boardSpaces[sr, sc]) != 1 && UM.getBit(redKings, boardSpaces[sr, sc]) != 1)
                {
                    Console.WriteLine("Invalid Move: There is No Red Piece at Start Row and Column");
                    return MI;
                }
                else
                {
                    if (UM.getBit(redPieces, boardSpaces[sr, sc]) == 1)
                    {
                        MI.isMovedPieceKing = false;
                    }
                    else if (UM.getBit(redKings, boardSpaces[sr, sc]) == 1)
                    {
                        MI.isMovedPieceKing = true;
                    }
                }
            }

            //Rule Check For End Space Being In Bounds. This is simplly done by checking whether the endRow or the endColumn
            //are less than 0 or greater than 7.
            if (ec > 7 || ec < 0 || er > 7 || er < 0)
            {
                Console.WriteLine("Invalid Move: Out of Bounds");
                return MI;
            }

            //Rule Check For End Space Being Occupied. This is done in a similar manner to checking if the start square has
            //a piece on it. The main difference is that this checks both the red and black pieces as if the end space is
            //alreaqdy occupied, it doesn't matter what color that occupying piece is.
            if (UM.getBit(blackPieces, boardSpaces[er, ec]) == 1 || UM.getBit(blackKings, boardSpaces[er, ec]) == 1 || UM.getBit(redPieces, boardSpaces[er, ec]) == 1 || UM.getBit(redKings, boardSpaces[er, ec]) == 1)
            {
                Console.WriteLine("Invalid Move: Cannot Move Into An Occupied Square");
                return MI;
            }

            //Rule Check To see if the end square is an actuall place the selected piece and legaly move. This is done by
            //creating a number of new bitboards, setting the bit that shares the same position as the selected piece to 1 for
            //each new board, and then making use of left/right bit shifting, or binary multiplication/division, to to get the
            //possible end squares the piece could move to.
            if (MI.isMovedPieceKing)
            {
                if (sc == 0 || sc == 7)
                {
                    UInt64 moveMap1 = 0, moveMap2 = 0, moveMap3 = 0, moveMap4 = 0;
                    moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap3 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap4 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                    if ((sr + 1) % 2 == 0)
                    {
                        moveMap1 <<= 9;
                        moveMap2 >>= 7;
                        moveMap3 <<= 18;
                        moveMap4 >>= 14;
                    }
                    else
                    {
                        moveMap1 <<= 7;
                        moveMap2 >>= 9;
                        moveMap3 <<= 14;
                        moveMap4 >>= 18;
                    }

                    if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1 &&
                        UM.getBit(moveMap3, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap4, boardSpaces[er, ec]) != 1)
                    {
                        Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                        return MI;
                    }

                }
                else
                {
                    UInt64 moveMap1 = 0, moveMap2 = 0, moveMap3 = 0, moveMap4 = 0, moveMap5 = 0, moveMap6 = 0, moveMap7 = 0, moveMap8 = 0;
                    moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap3 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap4 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap5 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap6 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap7 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                    moveMap8 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                    moveMap1 <<= 7;
                    moveMap2 >>= 7;
                    moveMap3 <<= 9;
                    moveMap4 >>= 9;
                    moveMap5 <<= 14;
                    moveMap6 >>= 14;
                    moveMap7 <<= 18;
                    moveMap8 >>= 18;

                    if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1 &&
                        UM.getBit(moveMap3, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap4, boardSpaces[er, ec]) != 1 &&
                        UM.getBit(moveMap5, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap6, boardSpaces[er, ec]) != 1 &&
                        UM.getBit(moveMap7, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap8, boardSpaces[er, ec]) != 1)
                    {
                        Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                        return MI;
                    }
                }
            }
            else
            {
                if(isBlackPlayer)
                {
                    if (sc == 0 || sc == 7)
                    {
                        UInt64 moveMap1 = 0, moveMap2 = 0;
                        moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                        if ((sr + 1) % 2 == 0)
                        {
                            moveMap1 <<= 9;
                            moveMap2 <<= 18;
                        }
                        else
                        {
                            moveMap1 <<= 7;
                            moveMap2 <<= 14;
                        }

                        if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1)
                        {
                            Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                            return MI;
                        }
                    }
                    else
                    {
                        UInt64 moveMap1 = 0, moveMap2 = 0, moveMap3 = 0, moveMap4 = 0;
                        moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap3 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap4 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                        moveMap1 <<= 7;
                        moveMap2 <<= 9;
                        moveMap3 <<= 14;
                        moveMap4 <<= 18;

                        if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1 &&
                            UM.getBit(moveMap3, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap4, boardSpaces[er, ec]) != 1)
                        {
                            Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                            return MI;
                        }
                    }
                }
                else
                {
                    if (sc == 0 || sc == 7)
                    {
                        UInt64 moveMap1 = 0, moveMap2 = 0;
                        moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                        if ((sr + 1) % 2 == 0)
                        {
                            moveMap1 >>= 7;
                            moveMap2 >>= 14;
                        }
                        else
                        {
                            moveMap1 >>= 9;
                            moveMap2 >>= 18;
                        }

                        if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1)
                        {
                            Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                            return MI;
                        }
                    }
                    else
                    {
                        UInt64 moveMap1 = 0, moveMap2 = 0, moveMap3 = 0, moveMap4 = 0;
                        moveMap1 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap2 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap3 = UM.setBit(moveMap1, boardSpaces[sr, sc]);
                        moveMap4 = UM.setBit(moveMap1, boardSpaces[sr, sc]);

                        moveMap1 >>= 7;
                        moveMap2 >>= 9;
                        moveMap3 >>= 14;
                        moveMap4 >>= 18;

                        if (UM.getBit(moveMap1, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap2, boardSpaces[er, ec]) != 1 &&
                            UM.getBit(moveMap3, boardSpaces[er, ec]) != 1 && UM.getBit(moveMap4, boardSpaces[er, ec]) != 1)
                        {
                            Console.WriteLine("Invalid Move: End Position Is Not A Valid Potential Move Location");
                            return MI;
                        }
                    }
                }
                

            }

            //Rule Check For If A Jump Move Is Possible. This check only occurs if the player enters a jump move with it
            //determining whether said jump was a legal move by seeing if an enemy piece is inbetween the start and end squares
            //If a legal jump is happening then it also get whether the jumped piece is a king or not and stores it in the MI
            //class variable.
            if (MathF.Abs(er - sr) > 1 || MathF.Abs(ec - sc) > 1)
            {

                MI.isJumping = true;
                if (MI.isMovedPieceKing)
                {
                    if(isBlackPlayer)
                    {
                        if (sc == 0)//If Piece is in the first column don't check to the Upper/Lower Left to prevent outofBounds error;
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc + 1]) == 1 ||
                        UM.getBit(redPieces, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc + 1]) == 1 ||
                            UM.getBit(redKings, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else if (sc == 7)//If Piece is in the 8th colum don't check to the Upper/Lower Right to prevent outofBounds error;
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc - 1]) == 1 ||
                        UM.getBit(redPieces, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc - 1]) == 1 ||
                            UM.getBit(redKings, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else//If the piece is anywhere else check all four diagonals
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc + 1]) == 1 ||
                        UM.getBit(redPieces, boardSpaces[sr - 1, sc - 1]) == 1 ||
                        UM.getBit(redPieces, boardSpaces[sr + 1, sc - 1]) == 1 ||
                        UM.getBit(redPieces, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc + 1]) == 1 ||
                            UM.getBit(redKings, boardSpaces[sr - 1, sc - 1]) == 1 ||
                            UM.getBit(redKings, boardSpaces[sr + 1, sc - 1]) == 1 ||
                            UM.getBit(redKings, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        
                    }
                    else
                    {
                        if (sc == 0)//If Piece is in the first colum don't check to the Upper/Lower Left to prevent outofBounds error;
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr - 1, sc + 1]) == 1 ||
                        UM.getBit(blackPieces, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr - 1, sc + 1]) == 1 ||
                            UM.getBit(blackKings, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else if (sc == 7)//If Piece is in the 8th colum don't check to the Upper/Lower Right to prevent outofBounds error;
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr - 1, sc - 1]) == 1 ||
                        UM.getBit(blackPieces, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr - 1, sc - 1]) == 1 ||
                            UM.getBit(blackKings, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else//If the piece is anywhere else check all four diagonals
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr - 1, sc + 1]) == 1 ||
                        UM.getBit(blackPieces, boardSpaces[sr - 1, sc - 1]) == 1 ||
                        UM.getBit(blackPieces, boardSpaces[sr + 1, sc - 1]) == 1 ||
                        UM.getBit(blackPieces, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr - 1, sc + 1]) == 1 ||
                            UM.getBit(blackKings, boardSpaces[sr - 1, sc - 1]) == 1 ||
                            UM.getBit(blackKings, boardSpaces[sr + 1, sc - 1]) == 1 ||
                            UM.getBit(blackKings, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        
                    }
                      
                }
                else
                {
                    if (isBlackPlayer)
                    {
                        if(sc == 0) //If Piece is in the first colum don't check to the Lower Left to prevent outofBounds error;
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else if(sc == 7) //If Piece is in the 8th colum don't check to the Lower Right to prevent outofBounds error;
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }   
                        else //If the pieces is anywhere else check both lower diagonals
                        {
                            if (UM.getBit(redPieces, boardSpaces[sr - 1, sc + 1]) == 1 || UM.getBit(redPieces, boardSpaces[sr - 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(redKings, boardSpaces[sr - 1, sc + 1]) == 1 || UM.getBit(redKings, boardSpaces[sr - 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        
                    }
                    else
                    {
                        if(sc == 0)//If Piece is in the first colum don't check to the Upper Left to prevent outofBounds error;
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr + 1, sc + 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else if(sc == 7)//If Piece is in the 8th colum don't check to the Upper Right to prevent outofBounds error;
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        else //If the pieces is anywhere else check both upper diagonals
                        {
                            if (UM.getBit(blackPieces, boardSpaces[sr + 1, sc + 1]) == 1 || UM.getBit(blackPieces, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = false;
                            }
                            else if (UM.getBit(blackKings, boardSpaces[sr + 1, sc + 1]) == 1 || UM.getBit(blackKings, boardSpaces[sr + 1, sc - 1]) == 1)
                            {
                                MI.isEnemyPieceKing = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Move: A Jump Is Not Possible");
                                MI.isJumping = false;
                                return MI;
                            }
                        }
                        
                        
                    }
                }
                
            }

            
            //This Rule Check sees if a normal red piece as made it to the 8th row or if a normal black piece has made it to the
            //1st row. If so the the isPromotionMove variable in the MI class variable gets turned to true so that the promotion
            //can occure in the movePiece method.
            if(!MI.isMovedPieceKing)
            {
                if (isBlackPlayer && er == 0 || !isBlackPlayer && er == 7)
                {
                    MI.isPromotionMove = true;
                }
                else
                {
                    MI.isPromotionMove = false;
                }
            }

            //An attempt at trying to implement being able to make a second jump if able after the first.
            //if(er < 6 && (!isBlackPlayer || (isBlackPlayer && MI.isMovedPieceKing)))
            //{
            //    if(ec <= 1)
            //    {
            //        if(MI.isMovedPieceKing)
            //        {
            //            if(isBlackPlayer)
            //            {
            //                if(UM.getBit(redPieces, boardSpaces[er - 1, ec + 1]) == 1 || 
            //                    UM.getBit(redPieces, boardSpaces[er + 1, ec + 1]) == 1 || 
            //                    UM.getBit(redKings, boardSpaces[er - 1, ec + 1]) == 1 || 
            //                    UM.getBit(redKings, boardSpaces[er + 1, ec + 1]) == 1)
            //                {
            //                    MI
            //                }
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else if (ec >= 6)
            //    {

            //    }
            //}
            //else if(er > 1 && (isBlackPlayer || (!isBlackPlayer && MI.isMovedPieceKing)))
            //{

            //}

            
            //If the method is able to reach this point then that means the move was a legal one.
            MI.isValidMove = true;
            return MI;
        }

    }
}