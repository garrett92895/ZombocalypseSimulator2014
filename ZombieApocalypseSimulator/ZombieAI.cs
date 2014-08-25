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
        public bool IntelligentAI { get; set; }
        public ZombieAI(bool IsIntelligentAI = false)
        {
            IntelligentAI = false;
        }

        public ActionTypes DecideAction(List<ActionTypes> PossibleActions, Character CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            ActionTypes action = 0;
            Zed zombie = (Zed)CurrentPlayer;

            if(IntelligentAI)
            {
                action = IntelligentAction(PossibleActions, zombie, MaxMoves, MovesLeft, Area, Players, Zombies);
            }
            else
            {
                action = DumbAction(PossibleActions, zombie, MaxMoves, MovesLeft, Area, Players, Zombies);
            }

            return action;
        }

        private ActionTypes IntelligentAction(List<ActionTypes> PossibleActions, Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            throw new NotImplementedException();
        }

        private ActionTypes DumbAction(List<ActionTypes> PossibleActions, Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            ActionTypes action = 0;

            if(CurrentPlayer.GetType() == typeof(Sloucher))
            {
                if(Area.AdjacentCharacters(CurrentPlayer, false).Any() 
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
            else if (CurrentPlayer.GetType() == typeof(Shank)
                || CurrentPlayer.GetType() == typeof(FastAttack))
            {
                if(Area.AdjacentCharacters(CurrentPlayer, false).Any()
                    && (!CurrentPlayer.HasAttacked || (CurrentPlayer.HasAttacked && MaxMoves == (int)(MovesLeft / 2))))
                {
                    action = ActionTypes.MeleeAttack;
                }
                else
                {
                    action = ActionTypes.Move;
                }
            }
            else if (CurrentPlayer.GetType() == typeof(Tank))
            {
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

        public Coordinate DetermineMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            Coordinate move = null;

            if(IntelligentAI)
            {
                move = IntelligentMove(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }
            else
            {
                move = DumbMove(CurrentPlayer, MaxMoves, MovesLeft, Area, Players, Zombies);
            }

            return move;
        }

        private Coordinate IntelligentMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            throw new NotImplementedException();
        }

        private Coordinate DumbMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            Coordinate bestMove = null;

            List<Coordinate> moves = Area.PossibleMovesForCharacter(CurrentPlayer, MovesLeft);
            List<KeyValuePair<Coordinate, Character>> enemies = new List<KeyValuePair<Coordinate, Character>>();

            foreach(Coordinate c in moves)
            {
                //The dummy character is only needed for its location, it won't actually exist in the game
                Character dummy = new Sloucher();
                dummy.Location = c;
                List<Character> possibleVictims = Area.AdjacentCharacters(dummy, false);

                //Adds them to the list of possible attacks
                foreach(Character victim in possibleVictims)
                {
                    enemies.Add(new KeyValuePair<Coordinate, Character>(c, victim));
                }
            }

            bestMove = enemies.ElementAt(0).Key;
            int CheapestMoveCost = int.MaxValue;
            foreach(KeyValuePair<Coordinate, Character> move in enemies)
            {
                int ThisMoveCost = Area.ShortestPathCost(CurrentPlayer, move.Key);
                if(ThisMoveCost < CheapestMoveCost)
                {
                    CheapestMoveCost = ThisMoveCost;
                    bestMove = move.Key;
                }
            }

            return bestMove;
        }


    }
}
