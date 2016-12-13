#r "Microsoft.Azure.WebJobs.Extensions.ApiHub"

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

public static void Run(Stream inputFile, Stream outputFile, TraceWriter log)
{
    log.Info("Image Process Starts");

    string locationText = GetCoordinate(inputFile, log);
    log.Info($"Text to be written: '{locationText}'");

    // Reset position. After Exif operations the cursor location is not on position 0 anymore;
    inputFile.Position = 0;

    WriteWatermark(locationText, inputFile, outputFile, log);

    log.Info("Image Process Ends");
}

private static string GetCoordinate(Stream image, TraceWriter log)
{
    log.Info("Extract location information");
    ExifReader exifReader = new ExifReader(image);
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

    return location;
}

private static void WriteWatermark(string watermarkContent, Stream originalImage, Stream newImage, TraceWriter log)
{
    log.Info("Write text to picture");
    using (Image inputImage = Image.FromStream(originalImage, true))
    {
        using (Graphics graphic = Graphics.FromImage(inputImage))
        {
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.DrawString(watermarkContent, new Font("Tahoma", 100, FontStyle.Bold), Brushes.Red, 200, 200);
            graphic.Flush();

            log.Info("Write to the output stream");
            inputImage.Save(newImage, ImageFormat.Jpeg);
        }
    }
}