<?php
// config.php
$servername = 'localhost';
$username = 'root';
$password = 'qwerty123';
$database = 'prueba1';
$conn = new mysqli($servername, $username, $password, $database);
if ($conn->connect_error) die("Error de conexión: " . $conn->connect_error);
?>