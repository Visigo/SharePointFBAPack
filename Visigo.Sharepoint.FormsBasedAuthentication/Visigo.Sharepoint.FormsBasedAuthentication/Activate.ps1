function DeactivateActivateFeature([string]$featureName)
{
	$feature = Get-SPFeature $featureName
	$features = [Microsoft.SharePoint.Administration.SPWebService]::ContentService.QueryFeatures($feature.id)
	foreach ($feature in $features)
	{
		Write-Host ("Deactivating/Activating Feature $featureName on " + $feature.Parent.Url)
		Disable-spfeature -identity $featureName -confirm:$false -url $feature.Parent.Url
		Enable-spfeature -identity $featureName -confirm:$false -url $feature.Parent.Url
	}
}


$url=$args[0]
$featureName="FBAManagement"
$solutionPath=$pwd.ToString() + "\" + $solutionName 
 
Add-PsSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue

#Restart the timer service to ensure the latest assembly is loaded
restart-service SPTimerV4

Write-Host 'Deactivating/activating active features to ensure activation script is run'
DeactivateActivateFeature($featureName)

if ($url)
{
	Write-Host 'Going to enable Feature' 
	Enable-spfeature -identity $featureName -confirm:$false -url $url
}