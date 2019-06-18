using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestRecordWin()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            renderer.Setup(r => r.Render(league)).Returns("Rendered League");

            var app = new App(league, renderer.Object, null);

            app.SendCommand("add player Alice");
            app.SendCommand("add player Bob");
            app.SendCommand("record win Alice Bob");

            Assert.AreEqual("Alice", app.SendCommand("winner"));
        }

        [TestMethod]
        public void TestSaveFile()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            var fileSave = new FileService();

            renderer.Setup(r => r.Render(league)).Returns("Rendered League");


            var app = new App(league, renderer.Object, fileSave);

            Assert.AreEqual("Saved test.csv", app.SendCommand("save test.csv"));

        }

        [TestMethod]
        public void TestLoadFile()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            var fileSave = new FileService();

            renderer.Setup(r => r.Render(league)).Returns("Rendered League");


            var app = new App(league, renderer.Object, fileSave);

            Assert.AreEqual("Loaded test.csv", app.SendCommand("load test.csv"));

        }
    }
}
