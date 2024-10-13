A project that entailed the creation of a checkers program through the use of bitboards and bitwise operations.

**Process:**
The process of creating this project was somewhat difficult as it was hard to find resource that were digestible to me, but I soon found a video detailing the creation of a chess bitboard project. Using that I started the project in C and was making good progress. However I soon ran into a problem that pushed me to migrate towards my perfered coding lanuage of C# and I continuted the project in Visual Stuidio. From then there were no more major hicupps.

**Classes and Functions:**
In my project I made use of three classes in total. These are:
  **Program:** The main class that handles all of the game logic.

  **UtilityClass:** A class that is full with various utility functions such as getting a bit, setting a bit, and getting the number of pieces that a bitboard is holding. A number of conversion methods that convert between decimal, binary, and hexadecimal are also in this class, as well as some binary arithmatic methods.

  **MoveInfo:** A class that holds a number of bools that store information about a move as it is being validated in the **Program** class.


There are also a few methods, particuarly in the Program class that I'd like to bring attention to. These are:
  initilizeBoards: A method that sets the redPieces, redKings, blackPieces, and blackKings bitboards to their proper values in order to represent a fresh new game of checkers. It also sets turnNum to 1 and makes a call to the next methos to show the board.

  boardShower: A method that takes all four of the previously mentioned bitboards and displays them as a 8x8 board with it displaying the number 1-8 for both the rows and columns. It also shows of the turn number, whose turn it is, as well as some other bits of information off to the right such as the numbe of pieces each player has as well as the binary or hexadecimal representation of the bitboards.

  GameLogic: The main method that handles the game loop which allows each player to take their turns and swaps control when said turns are over. It also keeps an eye out on the number of pieces each player has left and when that number reaches zero for one of the them the game is over.

  movePiece: A method that handles the movement of pieces around the board. It has a while loop that makes a call to the next method and if that call doesn't return a valid move the loop continues untill it does. After which the method will then go about manipulating the bit boards to represent the move that was just made.

  validateMove: A method that handles the validation of moves that it is handed by the movePiece method. This is done by taking the row and column number of the starting board square and the end board square and running them through a series of if statements. These if statements represent various rules that the move has to pass such as whether there is a piece at the start board square to begin with or seeing if the end board square is one that the piece would even be able to move to.
