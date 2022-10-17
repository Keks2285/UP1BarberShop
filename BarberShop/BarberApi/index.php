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
//echo "grkejhghreguioh"; die();
switch ($_SERVER['REQUEST_METHOD']){
    case "POST":

        switch ($params[1]){
            case "authorization": authorization($connect,$_POST); break;
            case "createEmploye": createemployee($connect,$_POST); break;  
            case "importEmploye": importemploye($connect); break;       
        }
    break;
    case "GET":{
        switch ($params[1]){
            case "getemployers": getemployers($connect,$_GET); break;
            case "getposts": getposts($connect,$_GET); break;
        }

    }



}
