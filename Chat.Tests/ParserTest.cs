﻿using System;
using System.Diagnostics.CodeAnalysis;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Chat.antlr.ast;
using Chat.antlr.ast.actions;
using Chat.antlr.grammar;
using Chat.antlr.parser;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace Chat.Tests
{
    [ExcludeFromCodeCoverage]
    public class ParserTest
    {
        public AST SetupParser(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            PlayerCommandsLexer lexer = new PlayerCommandsLexer(inputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            PlayerCommandsParser parser = new PlayerCommandsParser(tokens);

            ASTListener listener = new ASTListener();
            try
            {
                var parseTree = parser.input();
                ParseTreeWalker walker = new ParseTreeWalker();
                walker.Walk(listener, parseTree);
            }
            catch (ParseCanceledException e)
            {
                Assert.Fail(e.ToString());
            }

            return listener.getAST();
        }

        public static AST PickupCommand()
        {
            Input pickup = new Input();

            pickup.addChild(new Pickup());

            return new AST(pickup);
        }

        public static AST MoveForwardCommand()
        {
            Input moveForward = new Input();

            moveForward.addChild(new Move()
                .addChild(new Direction("forward"))
                .addChild(new Step(2)));

            return new AST(moveForward);
        }

        public static AST ExitCommand()
        {
            Input pickup = new Input();

            pickup.addChild(new Exit());

            return new AST(pickup);
        }

        public static AST AttackCommand(string direction)
        {
            Input attack = new Input();

            attack.addChild(new Attack()
                .addChild(new Direction(direction)));

            return new AST(attack);
        }

        public static AST DropCommand()
        {
            Input attack = new Input();

            attack.addChild(new Drop());

            return new AST(attack);
        }

        public static AST SayCommand(string message)
        {
            Input say = new Input();

            say.addChild(new Say()
                .addChild(new Message(message)));

            return new AST(say);
        }

        public static AST ShoutCommand(string message)
        {
            Input shout = new Input();

            shout.addChild(new Shout()
                .addChild(new Message(message)));

            return new AST(shout);
        }

        public static AST ReplaceCommand()
        {
            Input replace = new Input();

            replace.addChild(new Replace());

            return new AST(replace);
        }

        public static AST PauseCommand()
        {
            Input pause = new Input();

            pause.addChild(new Pause());

            return new AST(pause);
        }

        public static AST ResumeCommand()
        {
            Input resume = new Input();

            resume.addChild(new Resume());

            return new AST(resume);
        }

        [Test]
        public void DropCommandInputTest()
        {
            AST sut = SetupParser("drop");
            AST exp = DropCommand();

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void ExitCommandInputTest()
        {
            AST sut = SetupParser("exit");
            AST exp = ExitCommand();

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void AttackCommandWithDirectionAttacksForwardTest()
        {
            AST sut = SetupParser("attack forward");
            AST exp = AttackCommand("forward");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void AttackCommandWithLeftDirectionTest()
        {
            AST sut = SetupParser("attack left");
            AST exp = AttackCommand("left");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void AttackCommandWithRightDirectionTest()
        {
            AST sut = SetupParser("attack right");
            AST exp = AttackCommand("right");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void AttackCommandBackWardDirectionTest()
        {
            AST sut = SetupParser("attack backward");
            AST exp = AttackCommand("backward");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void PickupCommandTest()
        {
            AST sut = SetupParser("pickup");
            AST exp = PickupCommand();

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void SayCommandWithMessageTest()
        {
            AST sut = SetupParser("say \"hello world!\"");
            AST exp = SayCommand("\"hello world!\"");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void ShoutCommandWithMessageTest()
        {
            AST sut = SetupParser("shout \"hello world!\"");
            AST exp = ShoutCommand("\"hello world!\"");

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void ReplaceCommandTest()
        {
            AST sut = SetupParser("replace");
            AST exp = ReplaceCommand();

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void PauseCommandTest()
        {
            AST sut = SetupParser("pause");
            AST exp = PauseCommand();

            Assert.AreEqual(exp, sut);
        }

        [Test]
        public void ResumeCommandTest()
        {
            AST sut = SetupParser("resume");
            AST exp = ResumeCommand();

            Assert.AreEqual(exp, sut);
        }
    }
}