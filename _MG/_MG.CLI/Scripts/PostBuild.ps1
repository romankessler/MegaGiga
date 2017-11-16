<#
This script is used to deploy the nuget package
#>

$configName="Release" #$args[0]
$solutionDir=$PSScriptRoot+"\..\"

if($configName -like "Release")
{
    Copy-Item $solutionDir\bin\Release\*.dll $solutionDir\MegaGigaPackage\lib -Force
    & $solutionDir\MegaGigaPackage\nuget.exe pack $solutionDir\MegaGigaPackage\_MG.CLI.nuspec
    Move-Item $solutionDir\_MG.CLI.0.1.0.nupkg $solutionDir\MegaGigaPackage\ -Force
}