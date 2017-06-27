using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzEscapeHtmlListingExtractor
{
    class ListingWriter
    {

        public void WriteListing(Listing listing, string path)
        {
            string output = "---\n" +
                            $"title: {listing.Name}\n\n" +
                            $"listing-type: {listing.Type}\n" +
                            $"island: {listing.Island}\n" +
                            $"region: {listing.Region}\n\n" +
                            $"order: \n" +
                            $"image-path: {listing.Image}\n" +
                            $"image-alt: {listing.Name}\n" +
                            $"refer-url: {listing.Url}\n" +
                            "---\n" +
                            $"{listing.Description}";

            if (File.Exists(path))
                File.Delete(path);

            File.Create(path).Close();
            File.WriteAllText(path, output);
            Console.WriteLine($"File {path} created");
        }

    }
}
