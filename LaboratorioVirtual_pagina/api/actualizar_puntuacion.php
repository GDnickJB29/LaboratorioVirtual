<?php
session_start();
if (!isset($_SESSION['usuario'])) {
    echo "0";
    exit();
}
include 'config.php';

$usuario = $_SESSION['usuario'];
$result = $conn->query("SELECT score FROM usuarios WHERE nombre = '$usuario'");
if ($row = $result->fetch_assoc()) {
    echo $row['score'];
} else {
    echo "0";
}
?>
