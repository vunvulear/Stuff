$selectedSubscription = Get-AzureSubscription -Current
Write-Host Subscription in use: $selectedSubscription.SubscriptionName

Write-Host Start Deleting all Resource Groups

$selectedResourceGroups = Get-AzureRmResourceGroup
foreach($currentResourceGroup in $selectedResourceGroups)
{
	Write-Host Deleting Resource Group: $currentResourceGroup.ResourceGroupName
    Remove-AzureRmResourceGroup -Name $currentResourceGroup.ResourceGroupName -Force -Verbose
	Write-Host Deleted Resource Group:	$currentResourceGroup.ResourceGroupName
}

Write-Host Ended Deleting all Resource Groups
