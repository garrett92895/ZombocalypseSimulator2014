using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseSimulator
{
    [Serializable()]
    public class ZombieAI
    {
        #region Properties
        public bool IntelligentAI { get; set; }
        #endregion

        #region Ctor
        public ZombieAI(bool IsIntelligentAI = false)
        {
            IntelligentAI = IsIntelligentAI;
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
                    if (MovesLeft < (int)(MovesLeft / 2))
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
            else if (CurrentPlayer is Tank)
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

            //If a zombie has low health or is alone
            if((CurrentPlayer.Health / CurrentPlayer.MaxHealth < .2
                /*|| !HasSurroundingZombies(CurrentPlayer, Area, Zombies))*/
                && !(CurrentPlayer is Tank)))
            {
                action = ActionTypes.Move;
            }

            return action;
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
            else if (CurrentPlayer is Tank)
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
            Coordinate bestMove = null;

            if(CurrentPlayer.Health / CurrentPlayer.MaxHealth < .2
                || CurrentPlayer.HasAttacked)
            {
                bestMove = RunAway(CurrentPlayer, Area, MovesLeft, Players);
            }
            else if(!HasSurroundingZombies(CurrentPlayer, Area, Zombies))
            {
                bestMove = BestMoveToZombies(CurrentPlayer, Area, MovesLeft, Zombies);
            }
            else
            {
                bestMove = BestMoveToEnemy(CurrentPlayer, Area, MovesLeft, Players);
            }
            
            return bestMove;
        }

        private Coordinate DumbMove(Zed CurrentPlayer, int MaxMoves, int MovesLeft, GameArea Area, List<Character> Players, List<Character> Zombies)
        {
            Coordinate bestMove = null;

            List<Coordinate> moves = Area.PossibleMovesForCharacter(CurrentPlayer);
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
            Character victim = null;

            List<Character> Enemies = Area.AdjacentCharacters(CurrentPlayer, false);
            List<Character> PossibleKills = new List<Character>();
            List<Character> AttackChoices = new List<Character>();
            victim = Enemies.ElementAt(0);

            //Looks for kill opportunities
            foreach (Character c in Enemies)
            {
                if (ChanceOfKill(CurrentPlayer, c) > .9)
                {
                    PossibleKills.Add(c);
                }
            }
            

            //Deciding which list to loop through
            if (PossibleKills.Count() == 0)
            {
                AttackChoices = Enemies;
                foreach(Character c in Enemies)
                {
                    if(c.Health < victim.Health)
                    {
                        victim = c;
                    }
                }
            }
            else
            {
                AttackChoices = PossibleKills;
            }

            //Shanks
            if(CurrentPlayer is Shank)
            {
                //Looks for players with high SDC
                foreach (Character c in AttackChoices)
                {
                    if (c.SDC > victim.SDC)
                    {
                        victim = c;
                    }
                }
            }

            //Tanks
            if(CurrentPlayer is Tank)
            {
                //Looks for players that can't dodge
                foreach (Character c in AttackChoices)
                {
                    if (!c.CanDodge)
                    {
                        victim = c;
                    }
                }
            }

            return victim;
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

        #region Helper Methods

        private double ChanceOfKill(Zed Zombie, Character Victim)
        {
            int Chance = 0;
            int LOOP_FACTOR = 100;
            int DamageNeededToKill = Victim.SDC + Victim.Health;

            for(int i = 0; i < LOOP_FACTOR; i++)
            {
                if(Zombie.MeleeAttack().Damage > DamageNeededToKill)
                {
                    Chance += 1;
                }
            }

            //Probability of kill
            return Chance / LOOP_FACTOR;
        }

        private int SimulateAttack(Zed Zombie, Character Victim)
        {
            int AttackRoll = DieRoll.RollOne(20);
            int DefendRoll = DieRoll.RollOne(20);
            int Attack = Zombie.MeleeAttack().Damage;

            if(!Victim.CanParry && !Victim.CanDodge)
            {
                DefendRoll = 0;
            }
            else if (Zombie is Tank && !Victim.CanDodge)
            {
                DefendRoll = 0;
            }

            if(AttackRoll == 20 && DefendRoll != 20)
            {
                Attack *= 2;
            }
            else if(DefendRoll == 20 && AttackRoll != 20)
            {
                Attack = 0;
            }

            return Attack;
        }
        
        private bool HasSurroundingZombies(Zed Zombie, GameArea Area, List<Character> Zombies)
        {
            int NumOfSurroundingZombies = 0;
            int ZOMBIE_CLUSTER_FACTOR = 2;
            if(Zombies.Count() < ZOMBIE_CLUSTER_FACTOR)
            {
                ZOMBIE_CLUSTER_FACTOR = Zombies.Count();
            }

            if (Area.AdjacentCharacters(Zombie, true).Count() >= ZOMBIE_CLUSTER_FACTOR)
            {
                ++NumOfSurroundingZombies;
            }

            return NumOfSurroundingZombies > ZOMBIE_CLUSTER_FACTOR;
        }
      
        private Coordinate BestMoveToCharacter(Zed Zombie, Character DestinationCharacter, GameArea Area, int SquaresLeft)
        {
            Coordinate BestMove = null;

            int x = DestinationCharacter.Location.X;
            int y = DestinationCharacter.Location.Y;
            List<Coordinate> SurroundingCoordinates = new List<Coordinate>()
            {
                new Coordinate(x + 1, y),
                new Coordinate(x + 1, y + 1),
                new Coordinate(x, y + 1),
                new Coordinate(x - 1, y + 1),
                new Coordinate(x - 1, y),
                new Coordinate(x - 1, y - 1),
                new Coordinate(x, y - 1),
                new Coordinate(x + 1, y - 1)
            };

            BestMove = SurroundingCoordinates.ElementAt(0);
            foreach(Coordinate c in SurroundingCoordinates)
            {
                if(Area.ShortestPathCost(Zombie, c) < Area.ShortestPathCost(Zombie, BestMove)
                    && SquaresLeft >= Area.ShortestPathCost(Zombie, c))
                {
                    BestMove = c;
                }
            }

            return BestMove;
        }
       
        private Coordinate RunAway(Zed Zombie, GameArea Area, int SquaresLeft, List<Character> Players)
        {
            List<Coordinate> PossibleMoves = Area.PossibleMovesForCharacter(Zombie);
            Coordinate BestMove = PossibleMoves.ElementAt(0);

            foreach(Coordinate c in PossibleMoves)
            {
                foreach(Character Player in Players)
                {
                    if(Area.ShortestPathCost(Player, c) > Area.ShortestPathCost(Player, BestMove))
                    {
                        BestMove = c;
                    }
                }
            }

            return BestMove;
        }
       
        private Coordinate BestMoveToEnemy(Zed CurrentPlayer, GameArea Area, int MovesLeft, List<Character> Players)
        {
            Coordinate bestMove = null;

            List<Coordinate> moves = Area.PossibleMovesForCharacter(CurrentPlayer);
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

        private Coordinate BestMoveToZombies(Zed Zombie, GameArea Area, int SquaresLeft, List<Character> Zombies)
        {
            List<Coordinate> PossibleMoves = Area.PossibleMovesForCharacter(Zombie);
            Coordinate BestMove = PossibleMoves.ElementAt(0);

            foreach (Coordinate c in PossibleMoves)
            {
                foreach (Character z in Zombies)
                {
                    if (Area.ShortestPathCost(z, c) < Area.ShortestPathCost(z, BestMove))
                    {
                        BestMove = BestMoveToCharacter(Zombie, z, Area, SquaresLeft);
                    }
                }
            }

            return BestMove;
        }
        #endregion
    }
}
