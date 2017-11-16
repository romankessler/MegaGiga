$configName=$args[0]
$solutionDir=$PSScriptRoot

if($configName -like "Release")
{
Copy-Item $solutionDir\bin\Release\*.dll $solutionDir\MegaGigaPackage\lib -Force
nuget push $solutionDir\MegaGigaPackage\_MG.CLI.0.1.0.nupkg -Source https://www.myget.org/feed/Packages/mg
}