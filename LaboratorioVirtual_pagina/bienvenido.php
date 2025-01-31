<?php
session_start();
if (!isset($_SESSION['usuario'])) {
    header('Location: login.php');
    exit();
}
include 'api/config.php';

$usuario = $_SESSION['usuario'];
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bienvenido</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        let puntuacionActual = null;

        // Función para actualizar la puntuación automáticamente
        function actualizarPuntuacion() {
            $.get("api/actualizar_puntuacion.php", function(data) {
                if (puntuacionActual !== data) {
                    // Si la puntuación cambia y no ha sido actualizada manualmente, se pinta de rojo
                    $("#score").css("color", "red");
                }
            });
        }

        // Función para actualizar manualmente la puntuación y restaurar el color
        function actualizarPuntuacionManual() {
            $.get("api/actualizar_puntuacion.php", function(data) {
                $("#score").text(data).css("color", "black");  // Actualiza el valor y cambia el color a negro
                puntuacionActual = data;
            });
        }

        // Configuración para la actualización automática
        setInterval(actualizarPuntuacion, 1000);
    </script>
</head>
<body>
    <h1>Bienvenido <?php echo htmlspecialchars($usuario); ?></h1>
    <p>Puntuación: <span id="score">Cargando...</span></p>
    <button onclick="actualizarPuntuacionManual()">Actualizar puntuación</button>
    <iframe src="builds_unity/index.html" width="800" height="600"></iframe>
    <form action="api/logout.php" method="post">
        <button type="submit">Cerrar sesión</button>
    </form>
</body>
</html>
