<?php
session_start();
if (!isset($_SESSION['usuario'])) {
    echo json_encode(["status" => "error", "message" => "No hay sesión activa"]);
    exit();
}

include 'config.php';

$usuario = $_SESSION['usuario'];
$nuevoScore = isset($_POST['score']) ? intval($_POST['score']) : 0;

$conn->query("UPDATE usuarios SET score = $nuevoScore WHERE nombre = '$usuario'");

if ($conn->affected_rows > 0) {
    echo json_encode(["status" => "success", "message" => "Puntuación actualizada"]);
} else {
    echo json_encode(["status" => "error", "message" => "No se pudo actualizar"]);
}
?>
