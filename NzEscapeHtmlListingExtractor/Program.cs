using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NzEscapeHtmlListingExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // file location
            string[] htmlDirs = new string[]
            {
                @"C:\Users\Arthur\JEKYLL\nzescape.github.io-master\north-island",
                @"C:\Users\Arthur\JEKYLL\nzescape.github.io-master\south-island",
                @"C:\Users\Arthur\JEKYLL\nzescape.github.io-master\other-pages"
            };

            foreach (var dir in htmlDirs)
            {
                // filter out unnecessary files
                string[] htmlFiles = FilterFiles(dir);

                foreach (var file in htmlFiles)
                {
                    HtmlDocument htmlDoc = new HtmlDocument { OptionFixNestedTags = true };
                    htmlDoc.Load(file);

                    foreach (HtmlNode post in htmlDoc.DocumentNode.SelectNodes("//*[@class=\"post\"]"))
                    {
                        Listing listing = new Listing();

                        // start dancing around the fire from here
                        ExtractPostValuesIntoListing(post, listing);
                        SetIslandFromFilePath(file, listing);
                        SetRegionFromFileName(file, dir, listing);
                        SetListingType(file, listing);

                        WriteNewListing(listing, dir);
                    }
                }

                Console.WriteLine($"\n\nDirectory {dir} done!\n");
            }

            Console.WriteLine("All done..");
            Console.ReadLine();
        }

        private static string[] FilterFiles(string htmlDir)
        {
            return Directory.GetFiles(htmlDir)
                .Where(f => f.Contains("accommodation")
                || f.Contains("attractions")
                || f.Contains("campervans")
                || f.Contains("rental-cars")
                || f.Contains("tours"))
                .ToArray();
        }

        private static Listing ExtractPostValuesIntoListing(HtmlNode node, Listing listing)
        {
            var name = node.ChildNodes.FindFirst("h2").InnerText;
            var description = node.ChildNodes.FindFirst("p").InnerText;
            var url = node.ChildNodes.FindFirst("a").Attributes["href"].Value;
            var image = node.ChildNodes.FindFirst("img").Attributes["src"].Value.Replace("images/", "");

            listing.Name = name;
            listing.Description = description;
            listing.Url = url;
            listing.Image = image;

            return listing;
        }

        private static void WriteNewListing(Listing listing, string dir)
        {
            var subdir = "new-listings";
            var savePath = $"{dir}\\{subdir}\\{listing.Island}\\{listing.Region}\\";

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory($"{dir}\\{subdir}\\{listing.Island}\\{listing.Region}");

            // strip all non-alphanumeric characters
            var listingFileName = Regex.Replace(listing.Name, @"[^A-Za-z0-9 ]", "");
            listingFileName = listingFileName.ToLower()
                    .Trim()                     // trim whitespace in beginning and end
                        .Replace(" ", "-")      // replace spaces with dashes
                            + ".md";            // add extension

            var finalPath = savePath + listingFileName;

            // write file
            new ListingWriter().WriteListing(listing, finalPath);
        }

        private static Listing SetListingType(string filePath, Listing listing)
        {
            if (filePath.Contains("accommodation"))
                listing.Type = "accommodation";
            else if (filePath.Contains("attractions"))
                listing.Type = "attraction";
            else if (filePath.Contains("campervans || rental-cars"))
                listing.Type = "vehicle-rental";
            else if (filePath.Contains("tours"))
                listing.Type = "tour";
            else
                listing.Type = "unknown";

            return listing;
        }

        private static Listing SetRegionFromFileName(string filePath, string fileDir, Listing listing)
        {
            listing.Region = filePath.Remove(0, fileDir.Length + 1)
                .Replace("-accommodation", "")
                .Replace("-attractions", "")
                .Replace(".html", "");

            return listing;
        }

        private static Listing SetIslandFromFilePath(string filePath, Listing listing)
        {
            string island = String.Empty;

            if (filePath.Contains("north-island"))
                listing.Island = "north";
            else if (filePath.Contains("south-island"))
                listing.Island = "south";
            else
                listing.Island = "other";

            return listing;
        }
    }
}
