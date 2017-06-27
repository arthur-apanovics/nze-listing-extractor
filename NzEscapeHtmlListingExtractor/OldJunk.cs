//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NzEscapeHtmlListingExtractor
//{
//    class OldJunk
//    {
//        private static List<List<string[]>> PopulateMonsterContainer(string[] htmlFiles)
//        {
//            // (monster) container for all listings
//            List<List<string[]>> allRawListings = new List<List<string[]>>();

//            // get listings for each file in list of files and add to (monster) container
//            foreach (string file in htmlFiles)
//            {
//                allRawListings.Add(Program.GetListingRawLines(file));
//            }

//            return allRawListings;
//        }

//        private static void WriteListingsToFile(string htmlDir, List<List<string[]>> allRawListings)
//        {
//            if (File.Exists(htmlDir + "output.html"))
//                File.Delete(htmlDir + "output.html");

//            foreach (List<string[]> allListings in allRawListings)
//            {
//                foreach (string[] listingsArray in allListings)
//                {
//                    foreach (string listing in listingsArray)
//                    {
//                        File.AppendAllText(htmlDir + "output.html", listing + Environment.NewLine);
//                    }
//                }
//            }
//        }

//        private static List<string[]> GetListingRawLines(string input)
//        {
//            string divOpen = "<article class=\"post\">";
//            string divClose = "</div>";

//            var allLines = File.ReadAllLines(input);
//            List<string[]> listingsHtmlArray = new List<string[]>();

//            for (int i = 0; i < allLines.Length; i++)
//            {
//                if (allLines[i].Contains(divOpen))
//                {
//                    listingsHtmlArray.Add(allLines.Skip(i).Take(5).ToArray());
//                }
//            }
//            return listingsHtmlArray;
//        }
//    }
//}
