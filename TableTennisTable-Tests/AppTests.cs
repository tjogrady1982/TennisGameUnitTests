﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void TestPrintsCurrentState()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            renderer.Setup(r => r.Render(league)).Returns("Rendered League");

            var app = new App(league, renderer.Object, null);

            Assert.AreEqual("Rendered League", app.SendCommand("print"));
        }

        [TestMethod]
        public void TestAddPlayer()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            renderer.Setup(r => r.Render(league)).Returns("Rendered League");

            var app = new App(league, renderer.Object, null);

            app.SendCommand("add player Alice");

            var rows = league.GetRows();
            var firstRowPlayers = rows.First().GetPlayers();
            CollectionAssert.Contains(firstRowPlayers, "Alice");
        }

        [TestMethod]
        public void TestWinner()
        {

        }
    }
}
