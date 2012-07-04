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

Add-PsSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue
 
$url=$args[0]
$solutionName="Visigo.Sharepoint.FormsBasedAuthentication.wsp"
$featureName="FBAManagement"

$featureExists = Get-SPSolution $solutionName -ErrorAction SilentlyContinue

if ($featureExists)
{

	DeleteTimerJob($solutionName)

	if ($url)
	{
		$feature = Get-SPFeature -Site $url | where { $_.DisplayName -eq $featureName }

		if ($feature)
		{
			Write-Host 'Going to disable feature'
			disable-spfeature -identity $featureName -confirm:$false -url $url
		}
	}
 
	Write-Host 'Going to uninstall feature'
	uninstall-spfeature -identity $featureName -confirm:$false -force
 
	Write-Host 'Going to uninstall solution'
	Uninstall-SPSolution -identity $solutionName  -allwebapplications -confirm:$false

	Write-Host 'Waiting for job to finish'
	WaitForJobToFinish($solutionName)

	Write-Host 'Going to remove solution'
	Remove-SPSolution –identity $solutionName -confirm:$false

}
else
{
	Write-Host 'Solution not installed'
}
