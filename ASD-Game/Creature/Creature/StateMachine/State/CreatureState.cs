﻿using System;
using ASD_project.Creature.Creature.StateMachine.Data;

namespace ASD_project.Creature.Creature.StateMachine.State
{
    public abstract class CreatureState : IComparable
    {
        protected ICreatureData _creatureData;

        public CreatureState(ICreatureData creatureData)
        {
            _creatureData = creatureData;
        }

        public virtual void Do()
        {
            throw new NotImplementedException();
        }

        public virtual void Do(ICreatureData creatureData)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("State machine is not a comparable object.");
        }
    }
}