﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace Creature.Creature.StateMachine.Data
{
    public interface ICreatureData
    {
        bool IsAlive { get; }
        Vector2 Position { get; set; }
        int VisionRange { get; set; }
        double Health { get; set; }
        List<KeyValuePair<string, string>> RuleSet { get; }
    }
}