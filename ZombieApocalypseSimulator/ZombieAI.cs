using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseSimulator
{
    class ZombieAI
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



            return move;
        }

        private Coordinate IntelligentMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            throw new NotImplementedException();
        }

        private Coordinate DumbMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Player> Players, List<Zed> Zombies)
        {
            Coordinate move = null;



            return move;
        }


    }
}
