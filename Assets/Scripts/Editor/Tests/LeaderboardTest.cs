using InterfaceAdapters.Game.Level;
using NUnit.Framework;

namespace Editor.Tests
{
    public class LeaderboardTest
    {
        [Test]
        public void WhenEntryIsAddedFirst_TheLeaderboardIsUpdatedCorrectly()
        {
            var leaderboard = new LeaderboardBuilder().Build();
            var expectEntriesCount = leaderboard.Entries.Count;
            
            var entry = new LeaderboardEntry
            {
                Score = 100,
                UserName = "First"
            };
            leaderboard.AddEntry(entry, 0);
            
            Assert.AreEqual(expectEntriesCount, leaderboard.Entries.Count);
            Assert.AreEqual(entry.Score, leaderboard.Entries[0].Score);
            Assert.AreEqual(entry.UserName, leaderboard.Entries[0].UserName);
        }
        
        [Test]
        public void WhenEntryIsAddedAtMiddle_TheLeaderboardIsUpdatedCorrectly()
        {
            var leaderboard = new LeaderboardBuilder().Build();
            var expectEntriesCount = leaderboard.Entries.Count;
            var initialEntries = leaderboard.Entries;

            var newEntry = new LeaderboardEntry
            {
                Score = leaderboard.Entries[1].Score + 1,
                UserName = "2nd"
            };
            leaderboard.AddEntry(newEntry, 1);
            
            Assert.AreEqual(expectEntriesCount, leaderboard.Entries.Count);
            Assert.AreEqual(newEntry.Score, leaderboard.Entries[1].Score);
            Assert.AreEqual(newEntry.UserName, leaderboard.Entries[1].UserName);
            Assert.AreEqual(initialEntries[1].Score, leaderboard.Entries[2].Score);
            Assert.AreEqual(initialEntries[1].UserName, leaderboard.Entries[2].UserName);
        }
        
        [Test]
        public void WhenEntryIsAddedLast_TheLeaderboardIsUpdatedCorrectly()
        {
            var leaderboard = new LeaderboardBuilder().Build();
            var expectEntriesCount = leaderboard.Entries.Count;

            var newEntry = new LeaderboardEntry
            {
                Score = leaderboard.Entries[leaderboard.Entries.Count - 1].Score + 1,
                UserName = "Last"
            };
            leaderboard.AddEntry(newEntry, leaderboard.Entries.Count - 1);
            
            Assert.AreEqual(expectEntriesCount, leaderboard.Entries.Count);
            Assert.AreEqual(newEntry.Score, leaderboard.Entries[leaderboard.Entries.Count - 1].Score);
            Assert.AreEqual(newEntry.UserName, leaderboard.Entries[leaderboard.Entries.Count - 1].UserName);
        }
    }
}
