<?php
require "connect.php";
require "userfunctions.php";
mb_internal_encoding("UTF-8");
mb_internal_encoding('UTF-8');
mb_http_output('UTF-8');
mb_http_input('UTF-8');
mb_regex_encoding('UTF-8');
header('Content-type: application/json; charset=utf-8');
$params=explode('/', $_SERVER["PATH_INFO"]);
//echo '<p> grdfgrtdyht</p>'; die();
switch ($_SERVER['REQUEST_METHOD']){
    case "POST":

        switch ($params[1]){
            case "authorization": authorization($connect,$_POST); break;
            case "createEmploye": creatEemployee($connect,$_POST); break;  
            case "importEmploye": importEmploye($connect); break;    
            case "recoverPassword": recoverPassword($connect, $_POST); break;       
        }
    break;
    case "GET":{
        switch ($params[1]){
            case "getEmployers": getEmployers($connect,$_GET); break;
            case "getPosts": getPosts($connect,$_GET); break;
            case "getEmployeByEmail": getEmployeByEmail($connect,$_GET); break;
            case "getClientByEmail":  getClientByEmail($connect,$_GET); break;
        }

    }



}
