using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HypixelDataSearcher;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HypixelDataSearcher
{
    internal class PlayerInfo
    {
        private readonly HtmlDocument _document;

        public PlayerInfo()
        {
            _document = new HtmlDocument();
        }

        public async Task<bool> SetData(string nick)
        {
            var body = await HypixelRequester.GetBodyAsync(
                new HttpRequestMessage(HttpMethod.Get, $"https://hypixel.net/player/{nick}")
                );

            if (body == string.Empty)
            {
                return false;
            }

            _document.LoadHtml(body);
            return true;
        }

        public int GetFriendCount()
        {
            return GetIntStatisticFromPath(
                "html/body/div[1]/div[6]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/dl[3]/dd"
                );
        }

        public int GetBedWarsWins()
        {
            return GetIntStatisticFromPath(
                "html/body/div[1]/div[6]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table/tr[6]/td[2]"
                );
        }
        public int GetBedWarsLosses()
        {
            return GetIntStatisticFromPath(
                "html/body/div[1]/div[6]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table/tr[21]/td[2]"
                );
        }

        public string GetRank()
        {
            var text = GetStringStatisticFromPath(
                "html/body/div[1]/div[4]/div[1]/div[1]/h1/div[1]/span"
                );
            if (text == string.Empty)
            {
                return "player";
            }
            return text;
        }

        public int GetIntStatisticFromPath(string xPath)
        {
            var text = GetStringStatisticFromPath(xPath);

            var number = Convert.ToInt32(Regex.Replace(text, "\\D+", ""));
            return number;
        }

        public string GetStringStatisticFromPath(string xPath)
        {
            var element = _document.DocumentNode.SelectSingleNode(
                xPath
                );
            if (element is null)
            {
                return string.Empty;
            }
            return element.InnerText;
        }
    }
}
