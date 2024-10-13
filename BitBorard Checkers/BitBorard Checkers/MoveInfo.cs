using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBorard_Checkers
{
    internal class MoveInfo
    {
        public bool isValidMove; //Used to see if a move is valid
        public bool isJumping; //Used to see if a move is a jump
        public bool isMovedPieceKing; //Used to see if the piece currently being moved is a king or not
        public bool isEnemyPieceKing; //Used to see if the piece the current one is jumping over is a king or not
        public bool isPromotionMove; //Used to see if the move would result in a normal piece being promoted to a king.
    }
}
