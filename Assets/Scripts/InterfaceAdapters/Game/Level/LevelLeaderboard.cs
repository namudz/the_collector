using System;
using System.Collections.Generic;

namespace InterfaceAdapters.Game.Level
{
    [Serializable]
    public class LevelLeaderboard
    {
        public string LevelId;
        public List<LeaderboardEntry> Entries;

        public void InitializeEntries()
        {
            for (var i = 0; i < Entries.Capacity; ++i)
            {
                var entry = new LeaderboardEntry
                {
                    Score = -1,
                    UserName = string.Empty
                };
                Entries.Add(entry);
            }
        }

        public void AddEntry(LeaderboardEntry newEntry, int newEntryIndex)
        {
            var newEntries = new List<LeaderboardEntry>();
            
            if (newEntryIndex == 0)
            {
                newEntries.Add(newEntry);
                newEntries.AddRange(Entries);
                newEntries.RemoveAt(newEntries.Count - 1);
            }
            else if (newEntryIndex == Entries.Count)
            {
                newEntries.AddRange(Entries);
                newEntries.RemoveAt(Entries.Count);
                newEntries.Add(newEntry);
            }
            else
            {
                newEntries.AddRange(Entries);
                newEntries.RemoveRange(newEntryIndex, Entries.Count - newEntryIndex);
                newEntries.Add(newEntry);
                var currentEntriesCopy = new List<LeaderboardEntry>(Entries);
                currentEntriesCopy.RemoveRange(0, newEntryIndex);
                newEntries.AddRange(currentEntriesCopy);
                newEntries.RemoveAt(newEntries.Count - 1);
            }

            Entries = newEntries;
        }
    }
}