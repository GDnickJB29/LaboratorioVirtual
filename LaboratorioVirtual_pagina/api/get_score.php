<?php
session_start();
if (!isset($_SESSION['usuario'])) {
    echo json_encode(["score" => 0]);
    exit();
}

include 'config.php';

$usuario = $_SESSION['usuario'];
$result = $conn->query("SELECT score FROM usuarios WHERE nombre = '$usuario'");

if ($row = $result->fetch_assoc()) {
    echo json_encode(["score" => $row['score']]);
} else {
    echo json_encode(["score" => 0]);
}
?>
