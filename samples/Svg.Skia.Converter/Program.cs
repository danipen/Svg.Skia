﻿using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace Svg.Skia.Converter;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var optionInputFiles = new Option(new[] { "--inputFiles", "-f" }, "The relative or absolute path to the input files")
        {
            Argument = new Argument<FileInfo[]?>(getDefaultValue: () => null)
        };

        var optionInputDirectory = new Option(new[] { "--inputDirectory", "-d" }, "The relative or absolute path to the input directory")
        {
            Argument = new Argument<DirectoryInfo?>(getDefaultValue: () => null)
        };

        var optionOutputDirectory = new Option(new[] { "--outputDirectory", "-o" }, "The relative or absolute path to the output directory")
        {
            Argument = new Argument<DirectoryInfo?>(getDefaultValue: () => null)
        };

        var optionOutputFiles = new Option(new[] { "--outputFiles" }, "The relative or absolute path to the output files")
        {
            Argument = new Argument<FileInfo[]?>(getDefaultValue: () => null)
        };

        var optionPattern = new Option(new[] { "--pattern", "-p" }, "The search string to match against the names of files in the input directory")
        {
            Argument = new Argument<string?>(getDefaultValue: () => null)
        };

        var optionFormat = new Option(new[] { "--format" }, "The output image format")
        {
            Argument = new Argument<string>(getDefaultValue: () => "png")
        };

        var optionQuality = new Option(new[] { "--quality", "-q" }, "The output image quality")
        {
            Argument = new Argument<int>(getDefaultValue: () => 100)
        };

        var optionBackground = new Option(new[] { "--background", "-b" }, "The output image background")
        {
            Argument = new Argument<string>(getDefaultValue: () => "#00000000")
        };

        var optionScale = new Option(new[] { "--scale", "-s" }, "The output image horizontal and vertical scaling factor")
        {
            Argument = new Argument<float>(getDefaultValue: () => 1f)
        };

        var optionScaleX = new Option(new[] { "--scaleX", "-sx" }, "The output image horizontal scaling factor")
        {
            Argument = new Argument<float>(getDefaultValue: () => 1f)
        };

        var optionScaleY = new Option(new[] { "--scaleY", "-sy" }, "The output image vertical scaling factor")
        {
            Argument = new Argument<float>(getDefaultValue: () => 1f)
        };

        var optionSystemLanguage = new Option(new[] { "--systemLanguage" }, "The system language name as defined in BCP 47")
        {
            Argument = new Argument<string?>(getDefaultValue: () => null)
        };

        var optionQuiet = new Option(new[] { "--quiet" }, "Set verbosity level to quiet")
        {
            Argument = new Argument<bool>()
        };

        var optionLoadConfig = new Option(new[] { "--load-config", "-c" }, "The relative or absolute path to the config file")
        {
            Argument = new Argument<FileInfo?>(getDefaultValue: () => null)
        };

        var optionSaveConfig = new Option(new[] { "--save-config" }, "The relative or absolute path to the config file")
        {
            Argument = new Argument<FileInfo?>(getDefaultValue: () => null)
        };

        var rootCommand = new RootCommand
        {
            Description = "Converts a svg file to an encoded bitmap image."
        };

        rootCommand.AddOption(optionInputFiles);
        rootCommand.AddOption(optionInputDirectory);
        rootCommand.AddOption(optionOutputDirectory);
        rootCommand.AddOption(optionOutputFiles);
        rootCommand.AddOption(optionPattern);
        rootCommand.AddOption(optionFormat);
        rootCommand.AddOption(optionQuality);
        rootCommand.AddOption(optionBackground);
        rootCommand.AddOption(optionScale);
        rootCommand.AddOption(optionScaleX);
        rootCommand.AddOption(optionScaleY);
        rootCommand.AddOption(optionSystemLanguage);
        rootCommand.AddOption(optionQuiet);
        rootCommand.AddOption(optionLoadConfig);
        rootCommand.AddOption(optionSaveConfig);

        rootCommand.Handler = CommandHandler.Create(static (Settings settings, FileInfo? loadConfig, FileInfo? saveConfig) =>
        {
            SvgConverter.Execute(loadConfig, saveConfig, settings);
        });

        return await rootCommand.InvokeAsync(args);
    }
}
