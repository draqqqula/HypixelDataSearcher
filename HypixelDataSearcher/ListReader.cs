using Aspose.Cells;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypixelDataSearcher
{
    public static class ListReader
    {
        public static async Task<List<PlayerStat>> GetStatistics(string inputFile)
        {
            var result = new List<PlayerStat>();
            var nicknames = File.ReadAllLines(inputFile).Distinct().ToArray();
            foreach (var nickname in nicknames)
            {
                await Task.Delay(1000);
                var info = new PlayerInfo();
                var isSuccess = await info.SetData(nickname);

                if (!isSuccess)
                {
                    continue;
                }

                try
                {
                    var entry = new PlayerStat
                    {
                        FriendCount = info.GetFriendCount(),
                        WinCount = info.GetBedWarsWins(),
                        LossCount = info.GetBedWarsLosses(),
                        Rank = info.GetRank()
                    };
                    result.Add(entry);
                    Console.WriteLine($"Добавлена новая запись об игроке {nickname}. Записей {result.Count}/{nicknames.Length}");
                }
                catch (FormatException ex)
                {
                }
            }
            return result;
        }
    }

    public class PlayerStat()
    {
        public int FriendCount { get; init; }
        public int WinCount { get; init; }
        public int LossCount { get; init; }
        public string Rank { get; init; }
    }

    public static class DataExporter
    {
        public static void ExportToXlsx(IEnumerable<PlayerStat> entries, string outputFile)
        {
            var workBook = new Workbook();
            var workSheet = workBook.Worksheets[0];
            workSheet.Cells.ImportArray(["Число друзей", "Число побед", "Число поражений", "Ранг"], 0, 0, false);
            workSheet.Cells.ImportArray(entries.Select(it => it.FriendCount).ToArray(), 1, 0, true);
            workSheet.Cells.ImportArray(entries.Select(it => it.WinCount).ToArray(), 1, 1, true);
            workSheet.Cells.ImportArray(entries.Select(it => it.LossCount).ToArray(), 1, 2, true);
            workSheet.Cells.ImportArray(entries.Select(it => it.Rank).ToArray(), 1, 3, true);
            workBook.Save(outputFile);
        }
    }
}
