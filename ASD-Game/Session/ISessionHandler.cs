﻿using System.Collections.Generic;
using World.Models.Characters.Algorithms.NeuralNetworking.TrainingScenario;

namespace Session
{
    public interface ISessionHandler
    {
        public TrainingScenario TrainingScenario { get; set; }

        public bool JoinSession(string sessionId, string userName);

        public bool CreateSession(string sessionName, string userName);

        public void RequestSessions();

        public int GetSessionSeed();

        public List<string[]> GetAllClients();
    }
}