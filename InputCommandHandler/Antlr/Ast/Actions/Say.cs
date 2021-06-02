﻿using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace InputCommandHandler.Antlr.Ast.Actions
{
    public class Say : Command, IEquatable<Say>
    {
        private Message _message;
        public Message Message { get => _message; private set => _message = value; }

        public ArrayList GetChildren()
        {
            var children = new ArrayList();
            children.Add(_message);
            return children;
        }

        public override ASTNode AddChild(ASTNode child)
        {
            if (child is Message)
            {
                _message = (Message)child;
            }

            return this;
        }

        public ASTNode RemoveChild(ASTNode child)
        {
            if (child is Message && child == _message)
            {
                _message = null;
            }

            return this;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            return Equals(obj as Say);
        }

        [ExcludeFromCodeCoverage]
        public bool Equals(Say other)
        {
            if (other == null)
                return false;

            return _message.Equals(other._message);
        }
        public override int GetHashCode()
        {
            return _message.GetHashCode();
        }
    }
}