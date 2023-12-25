using HypixelDataSearcher;

var statistics = await ListReader.GetStatistics("players.txt");
DataExporter.ExportToXlsx(statistics, Path.Join("C:", "playerStats", "stats.xlsx"));