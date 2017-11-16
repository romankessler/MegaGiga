<#
This script is used to deploy the nuget package
#>

$configName=#$args[0]
$solutionDir=$PSScriptRoot+"\..\"

if($configName -like "Release")
{
    Copy-Item $solutionDir\bin\Release\*.dll $solutionDir\NugetPackage\lib -Force
    & $solutionDir\NugetPackage\nuget.exe pack $solutionDir\NugetPackage\_MG.CLI.nuspec
    Move-Item $solutionDir\_MG.CLI.0.1.0.nupkg $solutionDir\NugetPackage\ -Force
}