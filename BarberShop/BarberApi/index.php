<?php
require "connect.php";
require "userfunctions.php";
require "getMethods.php";
require "createMethods.php";
require "importMethods.php";
require "updateMethods.php";
require "removeMethods.php";
mb_internal_encoding("UTF-8");
mb_internal_encoding('UTF-8');
mb_http_output('UTF-8');
mb_http_input('UTF-8');
mb_regex_encoding('UTF-8');
header('Content-type: application/json; charset=utf-8');
$params=explode('/', $_SERVER["PATH_INFO"]);


//echo "Hello world";

switch ($_SERVER['REQUEST_METHOD']){
    case "POST":{

        switch ($params[1]){
            case "authorization": authorization($connect,$_POST); break;
            case "authorizationClient": authorizationClient($connect,$_POST); break;
            case "createEmploye": creatEemployee($connect,$_POST); break;  
            case "importEmploye": importEmploye($connect); break;    
            case "recoverPassword": recoverPassword($connect, $_POST); break;  
            case "removeEployerByEmail": removeEployerByEmail($connect,$_POST); break;
            case "removePost": removePost($connect,$_POST); break;  
            case "removeVacation": removeVacation($connect,$_POST); break;  
            case "removeProvider":removeProvider($connect, $_POST);break;
            case "removeStock":removeStock($connect, $_POST);break;
            case "removeSupply":removeSupply($connect, $_POST);break;
            case "createPost": createPost($connect,$_POST); break;  
            case "createStock": createStock($connect,$_POST); break; 
            case "createService": createService($connect,$_POST); break; 
            case "createProvider": createProvider($connect,$_POST); break; 
            case "registrateClient": registrateClient($connect,$_POST); break; 
            case "createRecord": createRecord($connect,$_POST); break; 
            case "createConsumption": createConsumption($connect,$_POST); break; 
            case "createIncome": createIncome($connect,$_POST); break; 
            case "createSupply": createSupply($connect,$_POST); break; 
            case "createSickLeave": createSickLeave($connect,$_POST); break; 
            case "createVacation": createVacation($connect,$_POST); break; 
            case "createMaterial": createMaterial($connect,$_POST); break; 
            case "createTaxReport": createTaxReport($connect,$_POST); break;
            case "updatePost": updatePost($connect,$_POST); break;
            case "updateEmploye": updateEmploye($connect,$_POST); break;
            case "updateStock": updateStock($connect,$_POST); break;
            case "updateSupply": updateSupply($connect,$_POST); break;
            case "updateProvider": updateProvider($connect,$_POST); break;
            case "executeBackup": executeBackup($connect); break;
        }
    }
    break;
    case "GET":{
        switch ($params[1]){
            case "getEmployers": getEmployers($connect); break;
            case "getPosts": getPosts($connect,$_GET); break;
            case "getEmployeByEmail": getEmployeByEmail($connect,$_GET); break;
            case "getClientByEmail":  getClientByEmail($connect,$_GET); break;
            case "getStatuses":  getStatuses($connect); break;
            case "getStocks":  getStocks($connect); break;
            case "getServices":  getServices($connect); break;
            case "getProviders":  getProviders($connect); break;
            case "getClients":  getClients($connect); break;
            case "getRecords":  getRecords($connect); break;
            case "getConsumptions":  getConsumptions($connect); break;
            case "getIncomes":  getIncomes($connect); break;
            case "getSupplies":  getSupplies($connect); break;
            case "getSickLeaves":  getSickLeaves($connect); break;
            case "getVacations":  getVacations($connect); break;
            case "getMaterials":  getMaterials($connect); break;
            case "getTaxReports":  getTaxReports($connect); break;
        }

    }

}
?>

