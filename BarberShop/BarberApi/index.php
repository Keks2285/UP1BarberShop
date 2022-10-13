<?php
require "connect.php";
require "userfunctions.php";
header('Content-type: application/json');
$params=explode('/', $_SERVER["PATH_INFO"]);
switch ($_SERVER['REQUEST_METHOD']){
case "POST":

    switch ($params[1]){
        case "authorization": authorization($connect,$_POST); break;

    }
    break;




}
