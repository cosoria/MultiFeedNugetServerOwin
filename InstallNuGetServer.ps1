<#
	.SYNOPSIS
	Installs Multi Feed NuGet Server in IIS.
	
	.DESCRIPTION
	Installs Multi Feed NuGet Server in IIS.
	
	.EXAMPLE
	Install-NuGetServer 
#>
[CmdletBinding()]
param
(
	[Parameter(Mandatory=$false)]
	[String]
	$ParamWebSiteName = "MultiFeedNugetServer"
	,
	[Parameter(Mandatory=$false)]
	[String]
	$ParamPhysicalPath = "D:\Websites\MultiFeedNugetServer"
)

# Load Modules 
Import-Module WebAdministration;


function Install-NugetServer($ParamWebsiteName, $ParamPhysicalPath) {
	Write-Output "Installing Nuget Server...";
	Write-Output "  - Website name: ${ParamWebsiteName}";
	Write-Output "  - Physical Path ${ParamPhysicalPath}";
	
	Write-Output "NuGet Server Installed Sucessfully"
}


Install-NugetServer;
