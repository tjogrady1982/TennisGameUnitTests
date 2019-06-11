using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class LeagueTests
    {
        [TestMethod]
        public void TestAddPlayer()
        {
            // Given
            League league = new League();

            // When
            league.AddPlayer("Bob");

            // Then
            var rows = league.GetRows();
            Assert.AreEqual(1, rows.Count);
            var firstRowPlayers = rows.First().GetPlayers();
            Assert.AreEqual(1, firstRowPlayers.Count);
            CollectionAssert.Contains(firstRowPlayers, "Bob");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot add player Bob because they are already in the game")]
        public void TestAddDuplicatePlayer()
        {
            //Given
            League league = new League();

            // When
            league.AddPlayer("Bob");
            league.AddPlayer("Bob");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Player name %%% contains invalid")]
        public void TestAddPlayerNameWithInvalidCharacters()
        {
            //Given
            League league = new League();

            // When
            league.AddPlayer("%%%");
           
        }

        [TestMethod]
        public void TestGetRows()
        {
            // Given
            League league = new League();

            // When
            league.AddPlayer("Bob");
            league.AddPlayer("Ray");
            league.AddPlayer("Geoff");

            // Then
            var rows = league.GetRows();
            Assert.AreEqual(2, rows.Count);
        }

        [TestMethod]
        public void TestRecordWin()
        {
            // Given
            League league = new League();

            // When
            league.AddPlayer("Bob");
            league.AddPlayer("Ray");
            league.AddPlayer("Geoff");
            league.RecordWin("Geoff", "Bob");

            // Then
            var winner = league.GetWinner();
            Assert.AreEqual("Geoff", winner);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot record match result. Winner {winner} must be one row below loser {loser}")]
        public void TestRecordWinWhereRowsBetweenPlayersTooGreat()
        {
            //Given
            League league = new League();

            // When
            league.AddPlayer("Bob");
            league.AddPlayer("Ray");
            league.AddPlayer("Geoff");
            league.AddPlayer("Bill");
            league.RecordWin("Bill", "Bob");

        }
    }
}
