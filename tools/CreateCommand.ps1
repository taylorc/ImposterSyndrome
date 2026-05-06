
param([String]$featureName="",[String]$commandName="",[String]$entityName="" ) 


if($featureName -eq "" -or $commandName -eq "" -or $entityName -eq ""){
    Write-Error "Feature Name or Query Name or Entity Name not set"
    break
}

$currentPath = Get-Location

$newPath = "$currentPath/src/Application/UseCases"

Set-Location $newPath
$featurePath = "$newPath/$featureName"


if(-not (Test-Path $featurePath)){
    mkdir $featureName
}

Set-Location $featurePath


dotnet new ssw-ca-command --name $commandName --entityName $entityName --slnName "ImposterSyndrome"

Set-Location $currentPath


#mkdir {{FeatureName}}
#cd {{FeatureName}}
#dotnet new ssw-ca-query --name {{QueryName}} --entityName {{Entity}} --slnName {{SolutionName}}