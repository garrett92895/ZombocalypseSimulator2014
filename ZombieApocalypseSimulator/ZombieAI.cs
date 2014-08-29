using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseSimulator
{
    public class ZombieAI
    {
        #region Properties
        public bool IntelligentAI { get; set; }
        #endregion

        #region Ctor
        public ZombieAI(bool IsIntelligentAI = false)
        {
            IntelligentAI = false;
        }
        #endregion

        #region Deciding Actions
        public ActionTypes DecideAction(List<ActionTypes> PossibleActions, Character CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            ActionTypes action = 0;
            Zed zombie = (Zed)CurrentPlayer;

            if (IntelligentAI)
            {
                action = IntelligentAction(PossibleActions, zombie, MaxMoves, MovesLeft, Area, Players, Zombies);
            }
            else
            {
                action = DumbAction(PossibleActions, zombie, MaxMoves, MovesLeft, Area, Players, Zombies);
            }

            return action;
        }

        private ActionTypes IntelligentAction(List<ActionTypes> PossibleActions, Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines the action a zombie will take when Intelligent AI is disabled
        /// </summary>
        /// <param name="PossibleActions"></param>
        /// <param name="CurrentPlayer"></param>
        /// <param name="MaxMoves"></param>
        /// <param name="MovesLeft"></param>
        /// <param name="Area"></param>
        /// <param name="Players"></param>
        /// <param name="Zombies"></param>
        /// <returns></returns>
        private ActionTypes DumbAction(List<ActionTypes> PossibleActions, Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            ActionTypes action = 0;

            if (CurrentPlayer is Sloucher)
            {
                /*IF
                 *  -There are nearby players
                 *  -The zombie hasn't yet attacked
                 *  -The zombie has not moved more than two spaces
                 *THEN
                 *  -The zombie is marked as having now attacked
                 *  -The method will return an attack as the zombies choice of action
                 */
                if (Area.AdjacentCharacters(CurrentPlayer, false).Any()
                    && !CurrentPlayer.HasAttacked
                    && MaxMoves - MovesLeft < 2)
                {
                    CurrentPlayer.HasAttacked = true;
                    action = ActionTypes.MeleeAttack;
                }
                else
                {
                    action = ActionTypes.Move;
                }

            }
            //Shanks and Fast Attack's have the same set of rules for their AIs
            else if (CurrentPlayer is Shank
                || CurrentPlayer is FastAttack)
            {
                /*IF
                 *  -There are nearby players
                 *  -The zombie hasn't yet attacked OR
                 *      -The zombie has attacked on his first turn without moving any spaces
                 *THEN
                 *  -The zombie is marked as having now attacked
                 *  -The method will return an attack as the zombie's choice of action
                 */
                if (Area.AdjacentCharacters(CurrentPlayer, false).Any()
                    && (!CurrentPlayer.HasAttacked || (CurrentPlayer.HasAttacked && MaxMoves == (int)(MovesLeft / 2))))
                {
                    /*IF
                     *  -The zombie has moved and attacked
                     *THEN
                     *  -Set the zombie as unable to attack next turn 
                     */
                    if(MovesLeft < (int)(MovesLeft / 2))
                    {
                        CurrentPlayer.HasAttacked = true;
                    }
                    action = ActionTypes.MeleeAttack;
                }
                else
                {
                    action = ActionTypes.Move;
                }
            }
            else if (CurrentPlayer.GetType() == typeof(Tank))
            {
                /*IF
                 *  -The zombie has a player in an adjacent square
                 *  -The zombie hasn't yet attacked
                 *THEN
                 *  -Set the zombie as unable to attack next turn
                 *  -Return an attack as the zombie's choice of action
                 */
                if (Area.AdjacentCharacters(CurrentPlayer, false).Any()
                    && !CurrentPlayer.HasAttacked)
                {
                    CurrentPlayer.HasAttacked = true;
                    action = ActionTypes.MeleeAttack;
                }
                else
                {
                    action = ActionTypes.Move;
                }
            }

            return action;
        }
        #endregion

        #region Deciding best moves
        public Coordinate DetermineMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            Coordinate move = null;

            if (IntelligentAI)
            {
                move = IntelligentMove(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }
            else
            {
                move = DumbMove(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }

            return move;
        }

        private Coordinate IntelligentMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            throw new NotImplementedException();
        }

        private Coordinate DumbMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            Coordinate bestMove = null;

            List<Coordinate> moves = Area.PossibleMovesForCharacter(CurrentPlayer, MovesLeft);
            List<KeyValuePair<Coordinate, Character>> enemies = new List<KeyValuePair<Coordinate, Character>>();

            foreach (Coordinate c in moves)
            {
                //The dummy character is only needed for its location, it won't actually exist in the game
                Character dummy = new Sloucher();
                dummy.Location = c;
                List<Character> possibleVictims = Area.AdjacentCharacters(dummy, false);

                //Adds them to the list of possible attacks
                foreach (Character victim in possibleVictims)
                {
                    enemies.Add(new KeyValuePair<Coordinate, Character>(c, victim));
                }
            }

            if (enemies.Count() != 0)
            {
                bestMove = enemies.ElementAt(0).Key;
                int CheapestMoveCost = int.MaxValue;
                foreach (KeyValuePair<Coordinate, Character> move in enemies)
                {
                    int ThisMoveCost = Area.ShortestPathCost(CurrentPlayer, move.Key);
                    if (ThisMoveCost < CheapestMoveCost)
                    {
                        CheapestMoveCost = ThisMoveCost;
                        bestMove = move.Key;
                    }
                }
            }
            else
            {
                //bestMove = moves.ElementAt(new Random().Next(moves.Count()));
                if (Players.Count() > 0)
                {
                    Players.ElementAt(new Random().Next(Players.Count()));
                    int CheapestMoveCost = int.MaxValue;
                    foreach (Coordinate c in moves)
                    {
                        int ThisMoveCost = Area.ShortestPathCost(CurrentPlayer, c);
                        if (ThisMoveCost < CheapestMoveCost)
                        {
                            CheapestMoveCost = ThisMoveCost;
                            bestMove = c;
                            break;
                        }
                    }
                }
            }

            return bestMove;
        }
        #endregion

        #region Deciding attacks
        public Character DetermineAttack(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            Character victim = null;

            if (IntelligentAI)
            {
                victim = IntelligentAttack(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }
            else
            {
                victim = DumbAttack(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }

            return victim;
        }

        private Character IntelligentAttack(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            throw new NotImplementedException();
        }

        private Character DumbAttack(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            Character victim = null;

            List<Character> Enemies = Area.AdjacentCharacters(CurrentPlayer, false);
            victim = Enemies.ElementAt(0);
            foreach (Character c in Enemies)
            {
                if (c.Health < victim.Health)
                {
                    victim = c;
                }
            }

            return victim;
        }

        #endregion
    }
}
