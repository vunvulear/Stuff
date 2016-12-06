#r "Microsoft.Azure.WebJobs.Extensions.ApiHub" // Don't forget to blog about it

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExifLib;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public static void Run(Stream inputFile, TraceWriter log, Stream outputFile)
{
    log.Info("Image Process Starts");

    log.Info("Extract location information");
    ExifReader exifReader = new ExifReader(inputFile);
    double[] latitudeComponents;
    exifReader.GetTagValue(ExifTags.GPSLatitude, out latitudeComponents);
    double[] longitudeComponents;
    exifReader.GetTagValue(ExifTags.GPSLongitude, out longitudeComponents);

    log.Info("Prepare string content");
    string location = string.Empty;
    if (latitudeComponents == null ||
        longitudeComponents == null)
    {
        location = "No GPS location";
    }
    else
    {
        double latitude = 0;
        double longitude = 0;
        latitude = latitudeComponents[0] + latitudeComponents[1] / 60 + latitudeComponents[2] / 3600;
        longitude = longitudeComponents[0] + longitudeComponents[1] / 60 + longitudeComponents[2] / 3600;

        location = $"Latitude: '{latitude}' | Longitude: '{longitude}'";
    }
    log.Info($"Text to be written: '{location}'");

    // Reset position. After Exif operations the cursor location is not on position 0 anymore;
    inputFile.Position = 0;

    log.Info("Write text to picture");
    using (Image inputImage = Image.FromStream(inputFile, true))
    {
        using (Graphics graphic = Graphics.FromImage(inputImage))
        {
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.DrawString(location, new Font("Tahoma", 100, FontStyle.Bold), Brushes.Red, 200, 200);
            graphic.Flush();

            log.Info("Write to the output stream");
            inputImage.Save(outputFile, ImageFormat.Jpeg);
        }
    }

    log.Info("Image Process Ends");
}
