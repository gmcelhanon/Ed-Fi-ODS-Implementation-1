# SPDX-License-Identifier: Apache-2.0
# Licensed to the Ed-Fi Alliance under one or more agreements.
# The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
# See the LICENSE and NOTICES files in the project root for more information.

function Reset-EmptyTemplateDatabase { 
<#
.description
    Runs Reset EmptyTemplateDatabase for LOAD THE ODS WITH SAMPLE XML DATA USING BULK LOAD CLIENT UTILITY
.parameter connectionString
    connectionString for the database where you want to reset .
    example $connectionString = 'database=EdFi_Ods_Empty_Template;data source=(local);trusted_connection=True'
.parameter engine
    engine should be either 'SQLServer', 'PostgreSQL'
.parameter database
    name of database 
.parameter filePaths
    list of filePaths
    example $filePaths= @('C:\oss-workspace\Ed-Fi-ODS','C:\oss-workspace\Ed-Fi-ODS-Implementation','C:\oss-workspace\Ed-Fi-ODS\Application\EdFi.Ods.Standard','C:\oss-workspace\Ed-Fi-ODS-Implementation\Application\EdFi.Ods.Extensions.TPDM')
#>
param(    
    [string]   $connectionString ='database=EdFi_Ods_Empty_Template;data source=(local);trusted_connection=True' ,
    [ValidateSet('SQLServer', 'PostgreSQL')]
    [string]   $engine =  'SQLServer',
    [string]   $database = 'Ods',        
    [string[]] $filePaths= @('C:\oss-workspace\Ed-Fi-ODS','C:\oss-workspace\Ed-Fi-ODS-Implementation','C:\oss-workspace\Ed-Fi-ODS\Application\EdFi.Ods.Standard','C:\oss-workspace\Ed-Fi-ODS-Implementation\Application\EdFi.Ods.Extensions.TPDM')
)
   Invoke-Task -name "Reset-EmptyTemplateDatabase" -task     $ResetEmptyTemplateDatabaseTasks                            
   
}

$ResetEmptyTemplateDatabaseTasks = {   
 
        $csb = New-Object System.Data.Common.DbConnectionStringBuilder    
        $csb.ConnectionString = $connectionString       

         $params = @{
            engine = $engine
            csb = $csb
            database = $database
            filePaths = $filePaths
            subTypeNames = @()
            dropDatabase = $true
        }
        Initialize-EdFiDatabaseWithDbDeploy @params
    }   

Reset-EmptyTemplateDatabase