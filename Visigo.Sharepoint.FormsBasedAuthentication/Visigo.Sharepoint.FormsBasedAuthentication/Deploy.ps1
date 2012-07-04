function WaitForJobToFinish([string]$SolutionFileName)
{ 
    $JobName = "*solution-deployment*$SolutionFileName*"
    $job = Get-SPTimerJob | ?{ $_.Name -like $JobName }
    if ($job -eq $null) 
    {
        Write-Host 'Timer job not found'
    }
    else
    {
        $JobFullName = $job.Name
        Write-Host -NoNewLine "Waiting to finish job $JobFullName"
        
        while ((Get-SPTimerJob $JobFullName) -ne $null) 
        {
            Write-Host -NoNewLine .
            Start-Sleep -Seconds 2
        }
        Write-Host  "Finished waiting for job.."
    }
}

function DeleteTimerJob([string]$SolutionFileName)
{ 
    $JobName = "*solution-deployment*$SolutionFileName*"
    $job = Get-SPTimerJob | ?{ $_.Name -like $JobName }
    if ($job -ne $null) 
    {
        Write-Host 'Existing Timer job found. Deleting'
		$job.Delete()
    }
}

function DeactivateActivateFeature([string]$featureName)
{
	$feature = Get-SPFeature $featureName
	$features = [Microsoft.SharePoint.Administration.SPWebService]::ContentService.QueryFeatures($feature.id)
	foreach ($feature in $features)
	{
		Write-Host ("Deactivating/Activating Feature $featureName on " + $feature.Parent.Url)
		Disable-spfeature -identity $featureName -confirm:$false -url $feature.Parent.Url -force
		Enable-spfeature -identity $featureName -confirm:$false -url $feature.Parent.Url -force
	}
}


$url=$args[0]
$solutionName="Visigo.Sharepoint.FormsBasedAuthentication.wsp"
$featureName="FBAManagement"
$solutionPath=$pwd.ToString() + "\" + $solutionName 
 
.\UnDeploy.ps1 $url
 
Add-PsSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue

DeleteTimerJob($SolutionName)

Write-Host 'Going to add solution'
Add-SPSolution $solutionPath
 
Write-Host 'Going to install solution to all web applications'
Install-SPSolution –identity $solutionName –allwebapplications –GACDeployment

Write-Host 'Waiting for job to finish' 
WaitForJobToFinish($SolutionName)

Write-Host 'Deactivating/activating active features to ensure activation script is run'
DeactivateActivateFeature($featureName)

if ($url)
{
	Write-Host 'Going to enable Feature' 
	Enable-spfeature -identity $featureName -confirm:$false -url $url
}