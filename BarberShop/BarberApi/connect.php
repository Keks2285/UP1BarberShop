<?php 
$host="192.168.1.49";


$connect = new PDO('mysql:host='."$host".';dbname=BarberShop;charset=utf8', 'root', '', [
PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC

]);