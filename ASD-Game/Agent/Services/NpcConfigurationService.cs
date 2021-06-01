﻿using Agent.Exceptions;
using System;
using System.Collections.Generic;
using Agent.Mapper;
using Agent.Models;
using InputHandling;
using Serilog;

namespace Agent.Services
{
    public class NpcConfigurationService : BaseConfigurationService
    {
        private List<Configuration> _npcConfigurations;
        private InputHandler _inputHandler;

        public NpcConfigurationService(List<Configuration> npcConfigurations, FileToDictionaryMapper fileToDictionaryMapper, InputHandler inputHandler)
        {
            _npcConfigurations = npcConfigurations;
            FileToDictionaryMapper = fileToDictionaryMapper;
            _inputHandler = inputHandler;
            FileHandler = new FileHandler();
            Pipeline = new Pipeline();
        }

        public override void CreateConfiguration(string npcName, string filepath)
        {
            var npcConfiguration = new NpcConfiguration();
            npcConfiguration.NpcName = npcName;
            npcConfiguration.Settings = FileToDictionaryMapper.MapFileToConfiguration(filepath);
            _npcConfigurations.Add(npcConfiguration);
        }

        public override List<Configuration> GetConfigurations()
        {
            return _npcConfigurations;
        }

        public override void Configure()
        {
            //TODO: Seems like duplicate code for now, but must be refactored later to match anticipated feature 'Configure NPC during a game'
            Console.WriteLine("What NPC do you wish to configure?");
            var npc = _inputHandler.GetCommand();
            if (npc.Equals(CANCEL_COMMAND))
            {
                return;
            }
            Console.WriteLine("Please provide code for the NPC");
            var code = _inputHandler.GetCommand();
            try
            {
                Pipeline.ParseString(code);
                Pipeline.CheckAst();
                var output = Pipeline.GenerateAst();
                string fileName = "npc/" + npc + "-config.cfg";
                FileHandler.ExportFile(output, fileName);
            }
            catch (SyntaxErrorException e)
            {
                LastError = e.Message;
                Log.Logger.Information("Syntax error: " + e.Message);
                Configure();
            }
            catch (SemanticErrorException e)
            {
                LastError = e.Message;
                Log.Logger.Information("Semantic error: " + e.Message);
                Configure();
            }
        }
    }
}