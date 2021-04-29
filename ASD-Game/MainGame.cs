using Microsoft.Extensions.Logging;
using System;
using System.Numerics;
using WorldGeneration;
using Player;
using Agent.Services;
using Chat;
using Creature;
using Creature.World;

namespace ASD_project
{
    partial class Program
    {
        public class MainGame : IMainGame
        {
            private readonly ILogger<MainGame> _log;

            public MainGame(ILogger<MainGame> log)
            {
                this._log = log;
            }

            public void Run()
            {
                Console.WriteLine("Game is gestart");

                // TODO: Remove from this method, team 2 will provide a command for it
                //AgentConfigurationService agentConfigurationService = new AgentConfigurationService();
                //agentConfigurationService.StartConfiguration();
                
                //moet later vervangen worden
                // ChatComponent chat = new ChatComponent();
                // PlayerModel playerModel = new PlayerModel();
                // do
                // {
                //     chat.HandleCommands(playerModel);
                // } while (true); // moet vervangen worden met variabele: isQuit 
                //
               // new WorldGeneration.Program();
                
                //Goep 4 NPC
                IWorld world = new DefaultWorld(25);
                ICreature player = new Creature.Player(10, new Vector2(3, 2));
                ICreature creature = new Monster(world, new Vector2(10, 10), 2, 6, 50);
                ICreature creature2 = new Monster(world, new Vector2(20, 20), 2, 6, 50);

                world.GenerateWorldNodes();
                world.SpawnPlayer(player);
                world.SpawnCreature(creature);
                world.SpawnCreature(creature2);

                world.Render();
                
                while (true)
                {
                    string input = Console.ReadLine();

                    MovePlayer(player, input);
                    world.Render();
                }
            }
            
            private static void MovePlayer(ICreature player, string input)
            {
                switch (input)
                {
                    case "w":
                        player.Position = new Vector2(player.Position.X, player.Position.Y + 1);
                        break;
                    case "a":
                        player.Position = new Vector2(player.Position.X - 1, player.Position.Y);
                        break;
                    case "s":
                        player.Position = new Vector2(player.Position.X, player.Position.Y - 1);
                        break;
                    case "d":
                        player.Position = new Vector2(player.Position.X + 1, player.Position.Y);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}